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
    [Range(20,100)]
    private float _zoomSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = transform;
        Vector3 newPosition = cameraTransform.position;
        newPosition += new Vector3(Input.GetAxisRaw("Horizontal") * _cameraSpeed * Time.deltaTime, Input.GetAxisRaw("Vertical") * _cameraSpeed * Time.deltaTime, 0);
        cameraTransform.position = newPosition;


        Camera camera = cameraTransform.GetComponent<Camera>();
        float cameraSizeModifier = (_invertedZoomScroll ? 1f : -1f) * Input.mouseScrollDelta.y * Time.deltaTime * Mathf.Clamp(_zoomSensitivity, 0, float.MaxValue);
        float newCameraSize = Mathf.Clamp(camera.orthographicSize + cameraSizeModifier, 0.1f, float.MaxValue);
        camera.orthographicSize = newCameraSize;
        
    }
}
