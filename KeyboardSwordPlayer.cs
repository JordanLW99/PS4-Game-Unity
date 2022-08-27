using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSwordPlayer : MonoBehaviour
{
    public float movementspeed = 2.5f;
    public Rigidbody playerrb;
    public Vector3 movement;
    public Animator playeranimation;

    public static bool stopplayer = false;

    public bool running, walking;

    public float rotationspeed = 12f;
    private float rotx;
    private float roty;
    public Camera cam;
    public GameObject player;

    public static int health = 100;
    public static bool attacking;
    public bool attackcheck;
    public static bool defending;
    public bool defendcheck;
    public static int gold;

    public static int damage;
    public static int upgradedamage;

    public float outofcombattimer;
    public float skilltimer;
    public bool skillcasted;
    public bool stopwhilecasting;

    public float canattackagain;

    //different buff will affect damage
    Buff swordbuff;
    float damageModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody>();
        playeranimation = GetComponentInChildren<Animator>();
        swordbuff = Buff.buff;
    }

    IEnumerator PlayerATK1()
    {
        attacking = true;
        playeranimation.SetBool("ATK2", true);
        yield return new WaitForSeconds(1.1f);
        playeranimation.SetBool("ATK2", false);
        attacking = false;
        canattackagain = 0;
    }

    IEnumerator PlayerPowerUp()
    {
        stopwhilecasting = true;
        playeranimation.SetBool("PowerUp", true);
        yield return new WaitForSeconds(2.2f);
        playeranimation.SetBool("PowerUp", false);
        stopwhilecasting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopplayer == false)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            movement.Set(0f, 0f, moveVertical);
            movement = movement.normalized * movementspeed * Time.deltaTime;
            movement = transform.worldToLocalMatrix.inverse * movement;

            if (Input.GetKey(KeyCode.W) && running == false)
            {
                playeranimation.SetBool("Walking", true);
            }

            if (Input.GetKey(KeyCode.S) && running == false)
            {
                playeranimation.SetBool("WalkingBackFromIdle", true);
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
            {
                running = false;
                movementspeed = 2.5f;
                playeranimation.SetBool("Running", false);
                playeranimation.SetBool("Walking", false);
                playeranimation.SetBool("WalkingBackFromIdle", false);
            }

            /*if (Input.GetButtonDown("Run") && Input.GetAxis("Vertical") > 0)
            {
                running = true;
                playeranimation.SetBool("Walking", false);
                playeranimation.SetBool("Running", true);
                movementspeed = 6f;
            }*/

            if (defending == true)
            {
                defendcheck = true;
            }

            if (defending == false)
            {
                defendcheck = false;
            }

            if (attacking == true)
            {
                attackcheck = true;
            }

            if (attacking == false)
            {
                attackcheck = false;
            }

            if (defending == true)
            {
                if (Input.GetButtonUp("Controller Defend"))
                {
                    defending = false;
                    playeranimation.SetBool("Defend", false);
                }
            }

            if (attacking == true)
            {
                canattackagain += Time.deltaTime;
            }

            if (attacking == false && defending == false && stopwhilecasting == false)
            {
                transform.Translate(new Vector3(0f, 0f, moveVertical) * (movementspeed) * Time.deltaTime);

                if (Input.GetButton("Controller Attack"))
                {
                    if (canattackagain == 0)
                    {
                        transform.Translate(new Vector3(0f, 0f, moveVertical) * (0) * Time.deltaTime);
                        damage = (int)(upgradedamage * DamageBuffCheck());
                        StartCoroutine(PlayerATK1());
                    }
                }


                if (Input.GetButton("Controller Defend"))
                {
                    defending = true;
                    transform.Translate(new Vector3(0f, 0f, moveVertical) * (0) * Time.deltaTime);
                    playeranimation.SetBool("Defend", true);
                }

                if (Input.GetButton("Skill"))
                {
                    if (health < 100)
                    {
                        if (skilltimer <= 0)
                        {
                            transform.Translate(new Vector3(0f, 0f, moveVertical) * (0) * Time.deltaTime);
                            StartCoroutine(PlayerPowerUp());
                            skillcasted = true;
                        }
                    }
                }
            }

            if (skillcasted == true)
            {
                swordbuff.SwordSkillCasted(30);
                skilltimer += Time.deltaTime;
                if (skilltimer < 10.5f)
                {
                    if (health < 100)
                    {
                        outofcombattimer += Time.deltaTime;
                        if (outofcombattimer >= 1.0f)
                        {
                            outofcombattimer = 0;
                            health = health + 1;
                        }
                    }
                }
                if (skilltimer >= 30.5f)
                {
                    outofcombattimer = 0;
                    skilltimer = 0;
                    skillcasted = false;
                }
            }

            rotx += Input.GetAxis("Mouse X") * rotationspeed;
            roty += Input.GetAxis("Mouse Y") * 0;
            cam.transform.localRotation = Quaternion.Euler(-roty, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, rotx, 0f);

            roty = Mathf.Clamp(roty, -22f, -22f);

            if (health <= 0)
            {
                attacking = true;
                playeranimation.SetBool("Death", true);
                stopplayer = true;
            }
        }
    }


    //potion buff check 
    float DamageBuffCheck()
    {
        damageModifier = 1.0f;
        if (Buff.damagebuff1on == true)
        {
            damageModifier = damageModifier * 1.25f;
        }
        if (Buff.damagebuff2on == true)
        {
            damageModifier = damageModifier * 1.50f;
        }
        if (Buff.damagebuff3on == true)
        {
            damageModifier = damageModifier * 2.0f;
        }
        return damageModifier;
    }
    //check end -- Yusong
}
