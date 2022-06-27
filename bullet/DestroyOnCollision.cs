using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetComponent<Rigidbody>().WakeUp();
        Invoke("HideBullet", 20.0f);
    }

    void HideBullet()
    {
        transform.GetComponent<Rigidbody>().Sleep();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}