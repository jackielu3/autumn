using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask pickableLayer;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pickupUI;
    [SerializeField] private BoxCollider pickupCollider;

    [Header("HitBox")]
    [SerializeField] private float range = 3;
    [SerializeField] private float width = 1;

    [Header("Objects")]
    [SerializeField][ReadOnly] private List<GameObject> currentPickables;
    [SerializeField][ReadOnly] private Tuple<GameObject, float> closestPickable;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Pickable")
        {
            currentPickables.Add(collider.gameObject);
            Pickable(collider.gameObject);
        }
    }

    private void Pickable(GameObject p)
    {
        float distance = Vector3.Distance(p.transform.position, gameObject.transform.position);
    }

}
