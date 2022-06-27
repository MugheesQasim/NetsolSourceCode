using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;

    void TakeDamage(float damage)
    {
        health -= damage;

        if(health<=0)
        {
            //Using pooling method to avoid deleting and then reinstantiating enemies
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("bullet"))
        {
            TakeDamage(40);
        }
    }
}
