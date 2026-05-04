using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private float zoomTarget;

    [SerializeField]
    private float multiplyer = 2f, minZoom = 1f, maxZoom = 10f, smoothTime = 0.1f;
    private float velocity = 0f;
    private Vector3 dragOrigin;
    [SerializeField] private CinemachineCamera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = FindFirstObjectByType<CinemachineCamera>();
        zoomTarget = cam.Lens.OrthographicSize;
    }

    // Update is called once per frame
    private void Update()
    {
        PanCamera();
        zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplyer;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
        cam.Lens.OrthographicSize = Mathf.SmoothDamp(cam.Lens.OrthographicSize, zoomTarget, ref velocity, smoothTime);
    }

    

    private void PanCamera()
    {
        if(Input.GetMouseButtonDown(0))
          dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print("oridin" + dragOrigin + "newPosition" + Camera.main.ScreenToWorldPoint(Input.mousePosition) + "difference" + difference);
            cam.transform.position += difference;
        }


    }
}
