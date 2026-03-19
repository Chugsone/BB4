using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TycoonCameraZoom : MonoBehaviour
{
    private float tycoonZoomTarget;

    [SerializeField]
    private float tycoonMultiplyer = 2f, tycoonMinZoom = 1f, tycoonMaxZoom = 10f, tycoonSmoothTime = 0.1f;
    private float tycoonVelocity = 0f;
    private Camera tycoonCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tycoonCam = GetComponent<Camera>();
        tycoonZoomTarget = tycoonCam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        tycoonZoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * tycoonMultiplyer;
        tycoonZoomTarget = Mathf.Clamp(tycoonZoomTarget, tycoonMinZoom, tycoonMaxZoom);
        tycoonCam.orthographicSize = Mathf.SmoothDamp(tycoonCam.orthographicSize, tycoonZoomTarget, ref tycoonVelocity, tycoonSmoothTime);
    }
}
