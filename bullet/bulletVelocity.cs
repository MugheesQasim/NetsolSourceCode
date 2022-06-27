using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletVelocity : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject[] enemies;
    private Vector3 closestEnemyLocation;

    [SerializeField] private float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
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

        Vector3 moveTowards = closestEnemyLocation - transform.position;
        moveTowards = moveTowards.normalized * Time.deltaTime * movementSpeed;

        float maxDistance = Vector3.Distance(rb.position, closestEnemyLocation);
        rb.MovePosition(rb.position + Vector3.ClampMagnitude(moveTowards, maxDistance));
    }
}
