using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class RotationByMouse : MonoBehaviour
{
    public bool isMouse;

    public Transform pointer;

    [Range(0, 1)]
    public float smoothSpeed;
    [Range(1, 10)]
    public float amountLook;

    private Camera cam;
    private Vector3 viewportCenter;
    private Vector3 startRotation;
    private float smoothX = 2.5f;
    private float smoothY = 2.5f;

    void Start()
    {
        cam = CameraManager.mainCamera;
        startRotation = transform.eulerAngles;
    }

    void Update()
    {
        float clamp = amountLook / 2;
        Vector2 mousePosition = cam.ScreenToViewportPoint(isMouse ? Input.mousePosition : pointer.position) * amountLook;

        smoothX = Mathf.SmoothStep(smoothX, mousePosition.x, smoothSpeed);
        smoothY = Mathf.SmoothStep(smoothY, mousePosition.y, smoothSpeed);

        viewportCenter = new Vector3(smoothX - clamp, smoothY - clamp, 0);

        transform.LookAt(viewportCenter);
    }

}
