using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothspeed;
    [SerializeField] private Vector3 offset;
    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -0f, 19.8f),
            Mathf.Clamp(transform.position.y, -0f, 0f),
            transform.position.z);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 positioncamera = player.localPosition + offset;
        Vector3 smoothCamera = Vector3.Lerp(transform.position, positioncamera, smoothspeed);
        transform.position = smoothCamera;
    }
}
