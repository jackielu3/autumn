using System.Collections.Generic;
using UnityEngine;
using static DynamicInventory;
using static Unity.VisualScripting.Member;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform gunTip;
    [SerializeField] private DynamicInventory playerInventory;

    [Header("Events")]
    public GameEvent onBulletDataChanged;

    [Header("Bullet Types")]
    // Currently manually setting selected bullet in editor on startup, but I need to find a way to change that....... ehe
    [SerializeField] private BulletData selectedBullet;
    [SerializeField] private int selectedBulletIndex = 0;
    [SerializeField] private List<ItemInstance> bulletInstances = new();

    private void Start()
    {
        UpdateBulletList();

        if (bulletInstances.Count > 0)
        {
            SelectBullet(selectedBulletIndex);
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
        if (selectedBullet != null && bulletInstances.Count > 0)
        {
            // Try to consume one bullet from the inventory
            if (playerInventory.TryConsume(selectedBullet, 1))
            {
                GameObject bullet = Instantiate(selectedBullet.model, gunTip.position, gunTip.rotation);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = gunTip.forward * selectedBullet.bulletForce;
                }

                // Notify UI of current selected bullet and count
                BulletDataChanged();
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
        BulletDataChanged();
    }

    private void BulletDataChanged()
    {
        var eventData = new DynamicInventory.InventoryEventData(
            selectedBullet,
            bulletInstances.Count > 0 ? bulletInstances[selectedBulletIndex].count : 0,
            0
        );

        // Debug.Log("UI Change Test: Item Type: " + selectedBullet + ", Count: " + eventData.count);

        onBulletDataChanged.Raise(this, eventData);
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

        BulletDataChanged();
        Debug.Log($"Updated bullet list with {bulletInstances.Count} items.");
    }

    public void UpdateBulletListEvent(Component sender, object data)
    {
        if (data is DynamicInventory.InventoryEventData eventData)
        {
            if (eventData.itemType is BulletData)
            {
                UpdateBulletList();
            }
        }
    }
}
 