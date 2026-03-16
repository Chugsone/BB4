using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private float zoomTarget;

    [SerializeField]
    private float multiplyer = 2f, minZoom = 1f, maxZoom = 10f, smoothTime = 0.1f;
    private float velocity = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
        zoomTarget = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplyer;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomTarget, ref velocity, smoothTime);
    }
}
