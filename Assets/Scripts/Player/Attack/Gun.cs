using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform gunTip;
    [SerializeField] GameObject bullet;

    [Header("Gun Logic")]
    [SerializeField] float bulletForce = 25f;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject b = Instantiate(bullet, gunTip.position, gunTip.rotation);

        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = gunTip.forward * bulletForce;
        }
    }

}
