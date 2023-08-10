using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOrigin;

    [SerializeField]
    private float zoomStep;

    private void Update()
    {
        cam = Camera.main;
        Pan();
        Zoom();
    }

    private void Pan()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(0))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += diff;
        }
    }

    private void Zoom()
    {
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomStep;
        if (cam.orthographicSize < 5)
            cam.orthographicSize = 5;
    }
}
