using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Look sensitivity")]
    public float sensX;
    public float sensY;

    [Header("Clamping")]
    public float minY;
    public float maxY;

    [Header("Spectator")]
    
    public float startSpectatorMoveSpeed;

    [Header("Current")]
    public float spectatorMoveSpeed;
    public float rotX;
    public float rotY;
    public bool isSpectator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        spectatorMoveSpeed = startSpectatorMoveSpeed;
    }
    public void LateUpdate()
    {
        // get mouse inputs
        rotX += Input.GetAxis("Mouse X") * sensX;
        rotY += Input.GetAxis("Mouse Y") * sensY;

        rotY = Mathf.Clamp(rotY, minY, maxY);

        // if we are dead
        if(isSpectator )
        {
            // rotate the cam vertically
            transform.rotation = Quaternion.Euler(-rotY, rotX, 0);

            // movement 
            float x = Input.GetAxis("Horizontal");
            float y = 0;
            float z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.E))
            {
                y = 1;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                y = -1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
               spectatorMoveSpeed= startSpectatorMoveSpeed * 2;
            }
            else if ( Input.GetKeyUp(KeyCode.LeftShift))
            {
                spectatorMoveSpeed = startSpectatorMoveSpeed;
            }
            Vector3 dir = transform.right*x + transform.up*y+transform.forward*z;

            transform.position += dir * spectatorMoveSpeed * Time.deltaTime;
        }

       
        //if we are good
        else
        {
            // look camrea up/down
            transform.localRotation = Quaternion.Euler(-rotY, 0, 0);
            // look camrea left/right
            transform.parent.rotation = Quaternion.Euler(transform.rotation.x, rotX, 0);
        }

    }
    



}
