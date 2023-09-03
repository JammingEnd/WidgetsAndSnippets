using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour  
{
    [SerializeField] private float lookSens;
    [SerializeField] private float smoothing;
    [SerializeField] private int MaxLookRotation;
    [SerializeField] private GameObject player;

    private Vector2 smoothedVelocity;
    private Vector2 currentLook;
    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
    }

    /// <summary>
    /// moves on the X and Y axis with a build-in unity move input
    /// 
    /// locks and rotates the camera when you move your mouse
    /// </summary>
    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSens * smoothing, lookSens * smoothing));
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLook += smoothedVelocity;

        currentLook.y = Mathf.Clamp(currentLook.y, -MaxLookRotation, MaxLookRotation);
        transform.localRotation = Quaternion.AngleAxis(-currentLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentLook.x, player.transform.up);
    }
}
