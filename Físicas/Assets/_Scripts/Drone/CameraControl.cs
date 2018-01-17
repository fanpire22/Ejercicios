using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class CameraControl : MonoBehaviour
{

    [SerializeField] FreeLookCam _camera;
    [SerializeField] ProtectCameraFromWallClip _protectFromWall;

    private void Awake()
    {
        _camera = GetComponent<FreeLookCam>();
        _protectFromWall = GetComponent<ProtectCameraFromWallClip>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool press = Input.GetKey(KeyCode.Mouse1);
        _camera.enabled = press;
        _protectFromWall.enabled = press;
    }
}
