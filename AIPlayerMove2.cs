using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMove2 : MonoBehaviour
{
    public float movementspeed = 2.5f;
    public Rigidbody playerrb;
    public Vector3 movement;
    public Animator playeranimation;

    public bool running;
    public bool walking;

    public float rotationspeed = 1f;
    private float rotx;
    private float roty;
    public Camera cam;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        playerrb = GetComponent<Rigidbody>();
        playeranimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool moveHorizontal = Input.GetAxisRaw("LeftJoystickX");
        /*float moveVertical = Input.GetButtonDown("Verticle");
        movement.Set(0f, 0f, moveVertical);
        movement = movement.normalized * movementspeed * Time.deltaTime;
        movement = transform.worldToLocalMatrix.inverse * movement;*/

        //transform.Translate(new Vector3(0f, 0f, moveVertical) * (movementspeed) * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) && running == false)
        {
            walking = true;
            transform.Translate(Vector3.forward * movementspeed * Time.deltaTime);
            playeranimation.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.LeftShift) && walking == true && Input.GetKey(KeyCode.W))
        {
            running = true;
            playeranimation.SetBool("Walking", false);
            playeranimation.SetBool("Running", true);
            movementspeed = 6f;
            transform.Translate(Vector3.forward * movementspeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            movementspeed = 2.5f;
            playeranimation.SetBool("Walking", true);
            playeranimation.SetBool("Running", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementspeed * Time.deltaTime);
            playeranimation.SetBool("WalkingBackFromIdle", true);
        }

        if (Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.W)))
        {
            walking = false;
            running = false;
            movementspeed = 2.5f;
            playeranimation.SetBool("Running", false);
            playeranimation.SetBool("Walking", false);
            playeranimation.SetBool("WalkingBackFromIdle", false);
        }

        player.transform.rotation = Quaternion.LookRotation(new Vector3(0f, rotx, 0f));

        rotx += Input.GetAxis("Mouse X") * rotationspeed;
        roty += Input.GetAxis("Mouse Y") * rotationspeed;
        //cam.transform.localRotation = Quaternion.Euler(-roty, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotx, 0f);

        roty = Mathf.Clamp(roty, -22f, -22f);
    }
}
