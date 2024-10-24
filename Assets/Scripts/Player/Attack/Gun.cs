using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform gunTip;

    [Header("Bullet Types")]
    [SerializeField] List<BulletData> bulletTypes;
    [SerializeField][ReadOnly] BulletData selectedBullet;

    private void Start()
    {
        selectedBullet = bulletTypes[0];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(selectedBullet.model, gunTip.position, gunTip.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = gunTip.forward * selectedBullet.bulletForce;
        }
    }

}
