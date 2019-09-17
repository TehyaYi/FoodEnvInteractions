using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _cameraSpeed;
    [SerializeField]
    private bool _invertedZoomScroll;
    [SerializeField]
    [Range(20, 100)]
    private float _zoomSensitivity;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float size = ModifySize();
        float sizeSpeedMultiplier = 0.25f * size;

        Vector3 newPosition = transform.position;
        newPosition += new Vector3(Input.GetAxisRaw("Horizontal") * _cameraSpeed * Time.deltaTime * sizeSpeedMultiplier,
                                    Input.GetAxisRaw("Vertical") * _cameraSpeed * Time.deltaTime * sizeSpeedMultiplier, 0);
        transform.position = newPosition;
    }

    private float ModifySize()
    {
        float cameraSizeModifier = 0;
        float newCameraSize = _camera.orthographicSize;

        if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.Minus))
        {
            if (Input.GetKey(KeyCode.Equals))
            {
                cameraSizeModifier -= Time.deltaTime * _zoomSensitivity;
            }
            else
            {
                cameraSizeModifier += Time.deltaTime * _zoomSensitivity;
            }
        }
        else
        {
            cameraSizeModifier = (_invertedZoomScroll ? 1f : -1f) * Input.mouseScrollDelta.y * Time.deltaTime * Mathf.Clamp(_zoomSensitivity, 0, float.MaxValue);
        }
        newCameraSize = Mathf.Clamp(_camera.orthographicSize + cameraSizeModifier, 0.1f, float.MaxValue);
        _camera.orthographicSize = newCameraSize;
        return _camera.orthographicSize;
    }
}
