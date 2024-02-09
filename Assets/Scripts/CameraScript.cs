using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    // Let's make a variable called target, so that we can target the player.
    // public Transform target;
    //private PlayerController access;

    // https://stackoverflow.com/questions/43709392/camera-always-behind-player-in-unity3d
    public GameObject player;
    public float cameraDistance = 30f;
    public float cameraHeight = 15f;
    public float cameraDistortion = 0;
    public bool isFloat = false;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(true);
        //access = Player.GetComponent<direction>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //cameraDistortion = player.transform.position.y / 30.0f + 1;
        if (cameraDistortion < 1)
        {
            cameraDistortion = 1;
        }

        // https://stackoverflow.com/questions/43709392/camera-always-behind-player-in-unity3d
        
        if (isFloat == false)
        {
            transform.position = player.transform.position - (player.transform.forward * cameraDistance);
            transform.LookAt(player.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y + (cameraHeight * cameraDistortion), transform.position.z);

            //Press the left ctrl key to look down
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Rotate(45, 0, 0);
            }
            //Press the Z key to look up
            if (Input.GetKey(KeyCode.Z))
            {
                transform.Rotate(-45, 0, 0);
            }
        } else {
            //Use the keys to move the camera around. We should also disable the player from being able to move.
            if (Input.GetKey(KeyCode.Space))
            {
            }
        }
        



        /*
        // Move the camera into position (Should use direction but having issues since direction is not one of the "main" variables.

        transform.position = target.position + new Vector3(-Mathf.Cos(player_direction * Mathf.PI/180) * 4f, 4f, -Mathf.Sin(player_direction * Mathf.PI/180) * 4f);// * cameraDistortion;

        //transform.position = target.position + new Vector3(target.rotation.y * 4f, 4f, target.rotation.y * 4f);  
        transform.LookAt(target);
        */
    }
}
