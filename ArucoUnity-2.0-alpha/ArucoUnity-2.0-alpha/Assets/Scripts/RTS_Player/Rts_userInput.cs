using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class Rts_userInput : MonoBehaviour {

    private Rts_player _player;

    private void Start()
    {
        _player = transform.root.GetComponent<Rts_player>();
    }

    private void Update()
    {
        if (_player.human)
        {
            MoveCamera();
            RotateCamera();
        }
    }

    private void MoveCamera()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;

        Vector3 movement = new Vector3(0, 0, 0); 

        // horizontal camera movement
        if(xpos >= 0 && xpos < Rts_resourceManager.ScrollWidth)
        {
            movement.x -= Rts_resourceManager.ScrollSpeed;
        } else if(xpos <= Screen.width && xpos > Screen.width - Rts_resourceManager.ScrollWidth){
            movement.x += Rts_resourceManager.ScrollSpeed;
        }

        // vertical camera movement
        if (ypos >= 0 && ypos < Rts_resourceManager.ScrollWidth)
        {
            movement.z -= Rts_resourceManager.ScrollSpeed;
        } else if (ypos <= Screen.height && ypos > Screen.height - Rts_resourceManager.ScrollWidth)
        {
            movement.z += Rts_resourceManager.ScrollSpeed;
        }

        // make sure movement is in the direction the camera is pointing 
        // but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        // away from ground movement
        movement.y = Rts_resourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        // calculate desired camera position based on the received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;

        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        // limit away from ground movement to be between a minimum and maximum distance
        if(destination.y > Rts_resourceManager.MaxCameraHeight)
        {
            destination.y = Rts_resourceManager.MaxCameraHeight;
        } else if(destination.y < Rts_resourceManager.MinCameraHeight)
        {
            destination.y = Rts_resourceManager.MinCameraHeight;
        }
    }

    private void RotateCamera()
    {

    }
}
