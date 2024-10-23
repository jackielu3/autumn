using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform gunTip;
    [SerializeField] GameObject bullet;
    
    private void Shoot()
    {
        Instantiate(bullet, gunTip, );
    }

}
