using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Standard Billboard script which makes canvas objects always look
    // at the camera
    private Camera _mainCam;
    private void Start() => _mainCam = Camera.main;

    void LateUpdate()
    {
        if (_mainCam) transform.LookAt(transform.position + _mainCam.transform.forward);
    }
}