using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICameraRotate : MonoBehaviour
{
    public float rotationspeed = 1f;
    private float rotx;
    private float roty;
    public Camera cam;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player.transform.rotation = Quaternion.LookRotation(new Vector3(0f, rotx, 0f));

        rotx += Input.GetAxis("RightJoystickX") * rotationspeed;
        roty += Input.GetAxis("RightJoystickY") * rotationspeed;
        cam.transform.localRotation = Quaternion.Euler(-roty, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotx, 0f);

        roty = Mathf.Clamp(roty, -22f, -22f);
    }
}
