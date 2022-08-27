using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public GameObject player, idle, swordplayer, daggerplayer, hammerplayer;
    public NavMeshAgent knight;
    public Animator knightanimations;
    public bool chase, playerdead, player2dead, forward, backward, isdead;
    public static bool knightattack1, knightattack2;
    private int enemyhealth = 90;
    public int enemygolddrop;
    public float timer;
    public GameObject parent;

    public bool leftright;
    public bool updown;
    public int randomwaittime;

    public BoxCollider[] swordcol;

    public HealthBar healthbar;//Y
    public Billboard healthbarbillboard;//Y
    public int Maxhp;//Y
    public GameObject losehealthtextprefab;//Y
    public GameObject pos;//Y

    // Start is called before the first frame update
    void Start()
    {
        swordcol = GetComponentsInChildren<BoxCollider>();
        Maxhp = enemyhealth;//Y
        healthbar.SetMaxHealth(Maxhp);//Y
        randomwaittime = Random.Range(4, 12);
        parent = transform.parent.gameObject;
        isdead = false;
        forward = true;
        playerdead = false;
        idle = parent.transform.GetChild(1).gameObject;
        knightanimations = GetComponent<Animator>();
        knight = GetComponent<NavMeshAgent>();
        knightattack1 = true;
    }

    IEnumerator PerformAttack1()
    {
        knightanimations.SetBool("CanAttack2", false);
        knightattack2 = false;
        knightanimations.SetBool("CanAttack", true);
        knight.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(1.3f);
        knightattack2 = true;
        knightattack1 = false;
    }
    IEnumerator PerformAttack2()
    {
        knightanimations.SetBool("CanAttack2", false);
        knightattack1 = false;
        knightanimations.SetBool("CanAttack2", true);
        knight.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(1.3f);
        knightattack1 = true;
        knightattack2 = false;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        //Destroy(parent);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSword" && AISCENEPLAYER.attacking == true)
        {
            enemyhealth = enemyhealth - AISCENEPLAYER.damage;
            healthbar.SetHealth(enemyhealth);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + AISCENEPLAYER.damage;//Y
            AISCENEPLAYER.attacking = false;
            if (enemyhealth <= 0)
            {
                isdead = true;
                foreach (Collider collider in swordcol)
                {
                    collider.enabled = false;
                }
                knight.GetComponent<NavMeshAgent>().isStopped = true;
                knightanimations.SetBool("Death", true);
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(6, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(3, 7);
                }
                AISCENEPLAYER.gold = AISCENEPLAYER.gold + enemygolddrop;
            }
        }

        if (other.gameObject.tag == "PlayerDagger" && AISCENEPLAYERDAGGERS.daggerattacking == true)
        {
            enemyhealth = enemyhealth - AISCENEPLAYERDAGGERS.daggerdamage;
            healthbar.SetHealth(enemyhealth);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + AISCENEPLAYERDAGGERS.daggerdamage ;//Y
            AISCENEPLAYERDAGGERS.daggerattacking = false;
            if (enemyhealth <= 0)
            {
                isdead = true;
                foreach (Collider collider in swordcol)
                {
                    collider.enabled = false;
                }
                knight.GetComponent<NavMeshAgent>().isStopped = true;
                knightanimations.SetBool("Death", true);
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(6, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(3, 7);
                }
                AISCENEPLAYERDAGGERS.daggergold = AISCENEPLAYERDAGGERS.daggergold + enemygolddrop;
            }
        }

        if (other.gameObject.tag == "PlayerHammer" && HammerPlayer.hammerattacking == true)
        {
            enemyhealth = enemyhealth - HammerPlayer.hammerdamage;
            healthbar.SetHealth(enemyhealth);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + HammerPlayer.hammerdamage;//Y
            HammerPlayer.hammerattacking = false;
            if (enemyhealth <= 0)
            {
                isdead = true;
                foreach (Collider collider in swordcol)
                {
                    collider.enabled = false;
                }
                knight.GetComponent<NavMeshAgent>().isStopped = true;
                knightanimations.SetBool("Death", true);
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(6, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(3, 7);
                }
                HammerPlayer.hammergold = HammerPlayer.hammergold + enemygolddrop;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerType.warrior == true)
        {
            player = swordplayer;
        }

        if (PlayerType.rogue == true)
        {
            player = daggerplayer;
        }

        if (PlayerType.hammer == true)
        {
            player = hammerplayer;
        }

        if (isdead == false)
        {
            knightanimations.SetBool("walk", true);
            knight.SetDestination(idle.transform.position);

            float distancetoplayer = Vector3.Distance(player.transform.position, transform.position);
            float distancetoidle = Vector3.Distance(idle.transform.position, transform.position);
            float playertopoint = Vector3.Distance(player.transform.position, idle.transform.position);

            if (distancetoplayer <= 10 && playertopoint <= 20)
            {
                if (playerdead == false)
                {
                    healthbarbillboard.Battle();//Y
                    knightanimations.SetBool("run", true);
                    knight.speed = 3.5f;
                    knight.SetDestination(player.transform.position);
                    knight.transform.LookAt(player.transform.position);
                }
                if (playerdead == true)
                {
                    healthbarbillboard.NotinBattle();//Y
                    knightanimations.SetBool("walk", true);
                    knightanimations.SetBool("run", false);
                    knight.SetDestination(idle.transform.position);
                    knight.speed = 2.0f;
                }
            }

            if (distancetoplayer <= 2f)
            {
                healthbarbillboard.Battle();//Y
                if (knightattack1 == true)
                {
                    StartCoroutine(PerformAttack1());
                }
                if (knightattack2 == true)
                {
                    StartCoroutine(PerformAttack2());
                }
            }

            if (distancetoplayer > 2f)
            {
                knightanimations.SetBool("CanAttack", false);
                knightanimations.SetBool("CanAttack2", false);
                knight.GetComponent<NavMeshAgent>().isStopped = false;
            }

            if (playertopoint > 20)
            {
                knightanimations.SetBool("run", false);
                knightanimations.SetBool("walk", true);
                knight.speed = 2f;
                healthbarbillboard.NotinBattle();//Y
                knight.SetDestination(idle.transform.position);
            }

            if (distancetoidle < 0.5f)
            {
                enemyhealth = 90;
                healthbar.SetHealth(enemyhealth);//Y
                knightanimations.SetBool("walk", false);
                timer += Time.deltaTime;
                if (updown == true)
                {
                    if (forward == true)
                    {
                        if (timer > randomwaittime)
                        {
                            timer = 0;
                            backward = true;
                            forward = false;
                            idle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 8);
                            knight.SetDestination(idle.transform.position);
                            knightanimations.SetBool("walk", true);
                        }
                    }
                    if (backward == true)
                    {
                        if (timer > randomwaittime)
                        {
                            timer = 0;
                            forward = true;
                            backward = false;
                            idle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8);
                            knight.SetDestination(idle.transform.position);
                            knightanimations.SetBool("walk", true);
                        }
                    }
                }

                if (leftright == true)
                {
                    if (forward == true)
                    {
                        if (timer > randomwaittime)
                        {
                            timer = 0;
                            backward = true;
                            forward = false;
                            idle.transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
                            knight.SetDestination(idle.transform.position);
                            knightanimations.SetBool("walk", true);
                        }
                    }
                    if (backward == true)
                    {
                        if (timer > randomwaittime)
                        {
                            timer = 0;
                            forward = true;
                            backward = false;
                            idle.transform.position = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);
                            knight.SetDestination(idle.transform.position);
                            knightanimations.SetBool("walk", true);
                        }
                    }
                }
            }

            if(PlayerType.rogue == true)
            {
                if (AISCENEPLAYERDAGGERS.daggerplayerhealth <= 0)
                {
                    playerdead = true;
                    knight.GetComponent<NavMeshAgent>().isStopped = false;
                    knight.SetDestination(idle.transform.position);
                    knightanimations.SetBool("CanAttack", false);
                    knightanimations.SetBool("CanAttack2", false);
                    knight.speed = 2f;
                }
                if (AISCENEPLAYERDAGGERS.daggerplayerhealth > 0)
                {
                    playerdead = false;
                }
            }

            if (PlayerType.warrior == true)
            {
                if (AISCENEPLAYER.health <= 0)
                {
                    playerdead = true;
                    knight.GetComponent<NavMeshAgent>().isStopped = false;
                    knight.SetDestination(idle.transform.position);
                    knightanimations.SetBool("CanAttack", false);
                    knightanimations.SetBool("CanAttack2", false);
                    knight.speed = 2f;
                }
                if (AISCENEPLAYER.health > 0)
                {
                    playerdead = false;
                }
            }

            if (PlayerType.hammer == true)
            {
                if (HammerPlayer.hammerhealth <= 0)
                {
                    playerdead = true;
                    knight.GetComponent<NavMeshAgent>().isStopped = false;
                    knight.SetDestination(idle.transform.position);
                    knightanimations.SetBool("CanAttack", false);
                    knightanimations.SetBool("CanAttack2", false);
                    knight.speed = 2f;
                }
                if (HammerPlayer.hammerhealth > 0)
                {
                    playerdead = false;
                }
            }
        }
        if (isdead == true)
        {
            StartCoroutine(Die());
        }
    }
}
