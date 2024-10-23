using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PlayerBaseAttack
{
    [SerializeField] Transform gunTipl;

    public Gun() : base("Gun", 1.0f, 50.0f) { }

    public void ExecuteAttack()
    {
        
    }

}
