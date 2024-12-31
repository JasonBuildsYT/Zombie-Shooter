using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera fullViewCamera;
    public Camera behindCamera;
    public Camera cinamaticCamera;

    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void ShowFullViewCamera()
    {
        behindCamera.enabled = false;
        cinamaticCamera.enabled = false;
        fullViewCamera.enabled = true;
    }

    public void ShowCinamaticCamera()
    {
        behindCamera.enabled = false;
        cinamaticCamera.enabled = true;
        fullViewCamera.enabled = false;
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void ShowBehindCamera()
    {
        behindCamera.enabled = true;
        cinamaticCamera.enabled = false;
        fullViewCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("[1]"))
            ShowFullViewCamera();
        if (Input.GetKeyDown("[2]"))
            ShowBehindCamera();
        if (Input.GetKeyDown("[3]"))
            ShowCinamaticCamera();
    }
}
