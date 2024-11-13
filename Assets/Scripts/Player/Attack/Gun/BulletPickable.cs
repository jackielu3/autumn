using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickable : MonoBehaviour
{
    public BulletData bulletData;

    void Start()
    {
        bulletData = this.GetComponent<BulletData>();
    }

    public void OnItemPickup(Component sender, object data)
    {
        
    }

}
