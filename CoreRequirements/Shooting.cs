using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private GameObject[] enemies;
    [SerializeField] private GameObject bulletProjectile;
    public GameObject povStartPoint;
    List<GameObject> bulletList;

    private Vector3 closestEnemyLocation;
    private Vector3 targetLocation;
    private Quaternion lookDirection;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        bulletList = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject objBullet = (GameObject)Instantiate(bulletProjectile);
            objBullet.SetActive(false);
            bulletList.Add(objBullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        closestEnemyLocation = transform.position;
        foreach (GameObject enemy in enemies)
        {

            Vector3 distancebtwenemies = enemy.transform.position - transform.position;
            if (distancebtwenemies.magnitude < closestEnemyLocation.magnitude)
            {
                closestEnemyLocation = enemy.transform.position;
            }
        }

        if (IsEnemyInFieldOfView(povStartPoint, closestEnemyLocation))
        {
            targetLocation = (closestEnemyLocation - transform.position);
            lookDirection = Quaternion.LookRotation(targetLocation);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, Time.deltaTime * rotationSpeed);

            if (Input.GetButtonDown("Jump"))
            {
                Fire();
            }
        }
        else
        {
            lookDirection = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, lookDirection, rotationSpeed * Time.deltaTime);
        }

    }
    bool IsEnemyInFieldOfView(GameObject Looker, Vector3 closestEnemy)
    {
        Vector3 lookVector = (closestEnemy - transform.position);
        float angle = Vector3.Angle(lookVector, Looker.transform.forward);

        if (angle < 90)
        {
            return true;
        }
        else
        {

        }
        return false;
    }

    void Fire()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].transform.position = transform.position;
                bulletList[i].transform.rotation = transform.rotation;
                bulletList[i].SetActive(true);
                break;
            }
        }
    }
}
