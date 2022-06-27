using System.Collections;
using Wrld;
using Wrld.Space;
using UnityEngine;

public class SpawnEnemiesBuildings : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab = null;

    private LatLongAltitude boxLocation1;
    private LatLong boxLocationL1;

    //[SerializeField]private Vector3[] boxLocationVectors;
    //private LatLongAltitude boxLocation2 = Api.Instance.SpacesApi.WorldToGeographicPoint(new Vector3(0, 10, -550));
    //private LatLongAltitude boxLocation3 = Api.Instance.SpacesApi.WorldToGeographicPoint(new Vector3(0, 10, -530));
    //private LatLongAltitude boxLocation4 = Api.Instance.SpacesApi.WorldToGeographicPoint(new Vector3(0, 10, -520));

    //private GameObject[] boxes;

    private void Start()
    { 

        /*for(int i=0;i<4;i++)
        {
            boxes[i] = Instantiate(boxPrefab);
        }*/
        //LatLong boxLocationL2 = LatLong.FromDegrees(boxLocation2.GetLatitude(), boxLocation2.GetLongitude());
        //LatLong boxLocationL3 = LatLong.FromDegrees(boxLocation3.GetLatitude(), boxLocation3.GetLongitude());
        //LatLong boxLocationL4 = LatLong.FromDegrees(boxLocation4.GetLatitude(), boxLocation4.GetLongitude());
    }
    private void Update()
    {

        if (GameObject.FindGameObjectWithTag("enemy") == null)
        {
            Vector3 location = new Vector3(33.7999992f, 34.7000008f, -420.5f);
            LatLongAltitude boxLocation1 = Api.Instance.SpacesApi.WorldToGeographicPoint(location);

            Debug.Log(boxLocation1);

            LatLong boxLocationL1 = LatLong.FromDegrees(boxLocation1.GetLatitude(), boxLocation1.GetLongitude());
            Debug.Log(boxLocationL1);
            MakeBox(boxLocationL1);
            //UpdatePosition(boxes[1],boxLocationL2);
            //UpdatePosition(boxes[2],boxLocationL3);
            //UpdatePosition(boxes[3],boxLocationL4);
        }
       
    }

    void MakeBox(LatLong latLong)
    {
        var ray = Api.Instance.SpacesApi.LatLongToVerticallyDownRay(latLong);
        LatLongAltitude buildingIntersectionPoint;
        Debug.Log(ray.direction);
        var didIntersectBuilding = Api.Instance.BuildingsApi.TryFindIntersectionWithBuilding(ray, out buildingIntersectionPoint);

        var boxAnchor = Instantiate(boxPrefab) as GameObject;
        boxAnchor.GetComponent<GeographicTransform>().SetPosition(latLong);

        var box = boxAnchor.transform.GetChild(0);
        Debug.Log((float)buildingIntersectionPoint.GetAltitude());
        box.localPosition = Vector3.up * (float)buildingIntersectionPoint.GetAltitude();
    }
}