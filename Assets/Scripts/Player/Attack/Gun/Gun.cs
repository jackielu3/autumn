using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform gunTip;

    [Header("Events")]
    public GameEvent onBulletCountChanged;
    public GameEvent onBulletSwitched;

    [Header("Bullet Types")]
    [SerializeField] private List<BulletData> bulletTypes;
    [SerializeField] private BulletData selectedBullet;
    [SerializeField] private int selectedBulletIndex;

    private void Start()
    {
        // TEMP UNTIL SAVE SYSTEM IS IMPLEMENTED
//        bulletTypes.ForEach(bullet => bullet.count = 10);

        selectedBullet = bulletTypes[selectedBulletIndex];
        onBulletSwitched.Raise(this, selectedBullet);
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            SwitchSelectedBullet(scroll);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
//        if (selectedBullet.count > 0)
//        {
            GameObject bullet = Instantiate(selectedBullet.model, gunTip.position, gunTip.rotation);

//            selectedBullet.count--;
//            onBulletCountChanged.Raise(this, selectedBullet.count);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = gunTip.forward * selectedBullet.bulletForce;
            }
//        }
    }

    private void SwitchSelectedBullet(float direction)
    {
        selectedBulletIndex = (selectedBulletIndex + 1) % bulletTypes.Count;
        selectedBullet = bulletTypes[selectedBulletIndex];
        onBulletSwitched.Raise(this, selectedBullet);
    }
}
 