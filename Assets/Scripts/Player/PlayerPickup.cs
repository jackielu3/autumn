using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[CreateAssetMenu(menuName = "GamemEvents/ItemPickupEvent")]
public class ItemPickupEvent : GameEvent { }

public class PlayerPickup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask pickableLayer;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pickupUI;
    [SerializeField] private BoxCollider pickupCollider;

//    [Header("HitBox")]
//    [SerializeField] private float range = 3;
//    [SerializeField] private float width = 1;

    [Header("Objects")]
    [SerializeField][ReadOnly] private List<GameObject> currentPickables;
    [SerializeField][ReadOnly] private Tuple<GameObject, float> closestPickable;

    [Header("Events")]
    [SerializeField] private ItemPickupEvent itemPickupEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Pickup();
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Pickable")
        {
            currentPickables.Add(collider.gameObject);
            Pickable(collider.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Pickable")
        {
            currentPickables.Remove(collider.gameObject);
        }
    }

    private void Pickable(GameObject p)
    {
        float distance = Vector3.Distance(p.transform.position, gameObject.transform.position);

        List<string> pickableNames = new List<string>();
        foreach(GameObject pickable in currentPickables)
        {
            pickableNames.Add(pickable.name);
        }
            
        Debug.Log("All pickable objects: " + string.Join(", ", pickableNames));
        Debug.Log("Pickable: " + p.name + ", Distance: " + distance);
    }

    private void Pickup()
    {
        foreach(GameObject pickable in currentPickables)
        {
            itemPickupEvent.Raise(this, pickable);
        }
    }

}
