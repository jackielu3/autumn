using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static DynamicInventory;
using static Unity.VisualScripting.Member;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform gunTip;
    [SerializeField] private DynamicInventory playerInventory;

    [Header("Events")]
    public GameEvent onBulletCountChanged;
    public GameEvent onBulletSwitched;

    [Header("Bullet Types")]
    [SerializeField] private BulletData selectedBullet;
    [SerializeField] private int selectedBulletIndex;
    [SerializeField] private List<ItemInstance> bulletInstances = new();

    private void Start()
    {
        UpdateBulletList();
        if (bulletInstances.Count > 0)
        {
            SelectBullet(0);
        }
        else
        {
            Debug.LogWarning("No bullets available in the inventory.");
            selectedBullet = null;
        }
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
        if (selectedBullet != null)
        {
            ItemInstance bulletInstance = bulletInstances[selectedBulletIndex];
            if (bulletInstance.count > 0)
            {
                // Instantiate the bullet and update inventory count
                GameObject bullet = Instantiate(selectedBullet.model, gunTip.position, gunTip.rotation);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = gunTip.forward * selectedBullet.bulletForce;
                }

                bulletInstance.count--;
                onBulletCountChanged.Raise(this, bulletInstance.count);
            }
            else
            {
                Debug.Log("Out of bullets!");
            }
        }
    }

    private void SwitchSelectedBullet(float direction)
    {
        selectedBulletIndex = (selectedBulletIndex + (direction > 0 ? 1 : -1) + bulletInstances.Count) % bulletInstances.Count;
        SelectBullet(selectedBulletIndex);
    }

    private void SelectBullet(int index)
    {
        selectedBulletIndex = index;
        selectedBullet = bulletInstances[index].itemType as BulletData;
        onBulletSwitched.Raise(this, selectedBullet);
    }

    private void UpdateBulletList()
    {
        bulletInstances.Clear();

        Category bulletCategory = playerInventory.GetCategory(typeof(BulletData));

        if (bulletCategory == null)
        {
            Debug.LogWarning("No bullets category found in inventory.");
            return;
        }

        foreach (ItemInstance item in bulletCategory.items)
        {
            if (item.itemType is BulletData)
            {
                bulletInstances.Add(item);
            }
        }

        Debug.Log($"Updated bullet list with {bulletInstances.Count} items.");

    }
}
 