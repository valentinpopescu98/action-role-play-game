using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Player player;
    public float pitch = 2f;
    public float yawSpeed = 100f;
    private float yaw = 0f;

    [Header("Offset:")]
    private Vector3 offset = new Vector3(0f, -0.8f, 0.5f);
    public Vector3 maxOffset;
    public Vector3 minOffset;

    [Header("Zoom:")]
    private float zoom = 15f;
    public float maxZoom = 15f;
    public float minZoom = 5f;

    // Update is called once per frame
    private void Update()
    {
        zoom -= 10 * Input.GetAxis("Mouse ScrollWheel");
        offset.z += Input.GetAxis("Mouse ScrollWheel");

        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        offset.z = Mathf.Clamp(offset.z, minOffset.z, maxOffset.z);

        if (Input.GetButton("Camera Yaw"))
            yaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        if (!player.dead)
        {
            transform.position = target.position - offset * zoom;
            transform.LookAt(target.position + Vector3.up * pitch);

            transform.RotateAround(target.position, Vector3.up, yaw);
        }
    }
}
