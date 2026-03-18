using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float zoomTarget;

    [SerializeField]
    private float multiplyer = 2f, minZoom = 1f, maxZoom = 10f, smoothTime = 0.1f;
    private float velocity = 0f;
    private Vector3 dragOrigin;
    [SerializeField] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
        zoomTarget = cam.orthographicSize;
    }

    // Update is called once per frame
    private void Update()
    {
        PanCamera();
        zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplyer;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomTarget, ref velocity, smoothTime);
    }

    

    private void PanCamera()
    {
        if(Input.GetMouseButtonDown(0))
          dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            print("oridin" + dragOrigin + "newPosition" + cam.ScreenToWorldPoint(Input.mousePosition) + "difference" + difference);
            cam.transform.position += difference;
        }


    }
}
