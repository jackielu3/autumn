using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float bottomClamp = -40f;
    [SerializeField] float topClamp = 70f;

    private float cinemachineTargetPitch;
    private float cinemachineTargetYaw;


    private void LateUpdate()
    {
        cameraLogic();
    }
    private void cameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");
        cinemachineTargetPitch = updateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        cinemachineTargetYaw = updateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);
        applyRotation(cinemachineTargetPitch, cinemachineTargetYaw);
    }

    private void applyRotation(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    private float updateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(string axis)
    { 
        return Input.GetAxis(axis) * rotationSpeed *Time.deltaTime; 
    }
}
