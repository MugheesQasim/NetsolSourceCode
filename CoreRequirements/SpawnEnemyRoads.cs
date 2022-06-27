using System.Collections;
using UnityEngine;
using Wrld;
using Wrld.Common.Maths;
using Wrld.Space;
using Wrld.Transport;

public class SpawnEnemyRoads : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab = null;
    private GameObject[] enemies;

    //saving latitude longitude values of the enemies to be placed on roads 
    private LatLongAltitude m_inputCoords1 = LatLongAltitude.FromDegrees(37.785668, -122.401270, 10.0);    
    private LatLongAltitude m_inputCoords2 = LatLongAltitude.FromDegrees(37.784468, -122.405128, 10.0);
    private LatLongAltitude m_inputCoords3 = LatLongAltitude.FromDegrees(37.782468, -122.405268, 10.0);

    private TransportPositioner m_transportPositioner;

    private void OnEnable()
    {
        //creating enemies here with the defined coordinates
        CreateEnemy(m_inputCoords1);
        CreateEnemy(m_inputCoords2);
        CreateEnemy(m_inputCoords3);
    }

    private void OnPointOnGraphChanged()
    {
        if (m_transportPositioner.IsMatched())
        {
            var pointOnGraph = m_transportPositioner.GetPointOnGraph();

            var outputLLA = LatLongAltitude.FromECEF(pointOnGraph.PointOnWay);
            const double verticalOffset = 2.0;
            outputLLA.SetAltitude(outputLLA.GetAltitude() + verticalOffset);
            var outputPosition = Api.Instance.SpacesApi.GeographicToWorldPoint(outputLLA);

            enemyPrefab.transform.position = outputPosition;

            enemyPrefab.SetActive(true);
        }
    }
    private void CreateEnemy(LatLongAltitude location)
    {
        var inputPosition = Api.Instance.SpacesApi.GeographicToWorldPoint(location);
        Instantiate(enemyPrefab);
        enemyPrefab.transform.localPosition = inputPosition;

        enemyPrefab.SetActive(true);
        var options = new TransportPositionerOptionsBuilder()
        .SetInputCoordinates(location.GetLatitude(), location.GetLongitude())
        .Build();

        m_transportPositioner = Api.Instance.TransportApi.CreatePositioner(options);
        m_transportPositioner.OnPointOnGraphChanged += OnPointOnGraphChanged;
    }

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            //whenever an object becomes inactive during the game, the following lines respawn them
            if(!enemy.activeInHierarchy)
            {
                StartCoroutine(RespawnWait(enemy));
            }
        }
    }

    //Enemies will be active again after 4s
    IEnumerator RespawnWait(GameObject enemy)
    {
        yield return new WaitForSeconds(4.0f);
        enemy.SetActive(true);
    }
}
