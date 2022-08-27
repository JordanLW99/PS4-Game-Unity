using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FarmEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject movingpoint, player, swordplayer, daggerplayer, hammerplayer;
    public float timer;
    private int farmenemyhp = 40;
    public Transform movingpointtrans;
    public bool forward, backward, reset, playerdead, player2dead, updown, leftright;
    public Animator farmenemyanimations;
    public static bool attack1, attack2;
    public bool isdead = false;
    public int enemygolddrop, randomwaittime;
    public GameObject parent;

    public HealthBar healthbar;//Y
    public Billboard healthbarbillboard;//Y
    public int Maxhp;//Y
    public GameObject losehealthtextprefab;//Y
    public GameObject pos;//Y

    public BoxCollider[] swordcol;

    // Start is called before the first frame update
    void Start()
    {
        Maxhp = farmenemyhp;//Y
        healthbar.SetMaxHealth(Maxhp);//Y
        randomwaittime = Random.Range(4, 12);
        parent = transform.parent.gameObject;
        forward = true;
        movingpoint = parent.transform.GetChild(1).gameObject;
        movingpointtrans = parent.transform.GetChild(1);
        enemy = GetComponent<NavMeshAgent>();
        farmenemyanimations = GetComponent<Animator>();
        attack1 = true;
    }

    IEnumerator PerformAttack1()
    {
        attack2 = false;
        farmenemyanimations.SetBool("attack2", false);
        farmenemyanimations.SetBool("attack1", true);
        enemy.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(1.3f);
        attack2 = true;
        attack1 = false;
    }
    IEnumerator PerformAttack2()
    {
        attack1 = false;
        farmenemyanimations.SetBool("attack2", false);
        farmenemyanimations.SetBool("attack2", true);
        enemy.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(1.3f);
        attack1 = true;
        attack2 = false;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSword" && AISCENEPLAYER.attacking == true)
        {
            AISCENEPLAYER.attacking = false;
            farmenemyhp = farmenemyhp - AISCENEPLAYER.damage;
            healthbar.SetHealth(farmenemyhp);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + AISCENEPLAYER.damage;//Y
            Debug.Log("Player Damage " + AISCENEPLAYER.damage);
            if (farmenemyhp <= 0)
            {
                foreach (Collider collider in swordcol)
                {
                    collider.enabled = false;
                }
                enemy.GetComponent<NavMeshAgent>().isStopped = true;
                farmenemyanimations.SetBool("death", true);
                isdead = true;
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(10, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(5, 6);
                }
                AISCENEPLAYER.gold = AISCENEPLAYER.gold + enemygolddrop;
                Debug.Log("You now have: " + AISCENEPLAYER.gold + " coins");
            }
        }

        if (other.gameObject.tag == "PlayerDagger" && AISCENEPLAYERDAGGERS.daggerattacking == true)
        {
            foreach (Collider collider in swordcol)
            {
                collider.enabled = false;
            }
            farmenemyhp = farmenemyhp - AISCENEPLAYERDAGGERS.daggerdamage;
            healthbar.SetHealth(farmenemyhp);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + AISCENEPLAYERDAGGERS.daggerdamage;//Y
            Debug.Log("Player Damage " + AISCENEPLAYERDAGGERS.daggerdamage);
            if (farmenemyhp <= 0)
            {
                enemy.GetComponent<NavMeshAgent>().isStopped = true;
                farmenemyanimations.SetBool("death", true);
                isdead = true;
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(10, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(5, 6);
                }
                AISCENEPLAYERDAGGERS.daggergold = AISCENEPLAYERDAGGERS.daggergold + enemygolddrop;
                Debug.Log("You now have: " + AISCENEPLAYERDAGGERS.daggergold + " coins");
            }
        }

        if (other.gameObject.tag == "PlayerHammer" && HammerPlayer.hammerattacking == true)
        {
            foreach (Collider collider in swordcol)
            {
                collider.enabled = false;
            }
            farmenemyhp = farmenemyhp - HammerPlayer.hammerdamage;
            healthbar.SetHealth(farmenemyhp);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + HammerPlayer.hammerdamage;//Y
            Debug.Log("Player Damage " + HammerPlayer.hammerdamage);
            if (farmenemyhp <= 0)
            {
                enemy.GetComponent<NavMeshAgent>().isStopped = true;
                farmenemyanimations.SetBool("death", true);
                isdead = true;
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(10, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(5, 6);
                }
                HammerPlayer.hammergold = HammerPlayer.hammergold + enemygolddrop;
                Debug.Log("You now have: " + HammerPlayer.hammergold + " coins");
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
            float distancetopoint = Vector3.Distance(movingpoint.transform.position, transform.position);
            float distancetoplayer = Vector3.Distance(player.transform.position, transform.position);
            float playertopoint = Vector3.Distance(player.transform.position, movingpoint.transform.position);

            farmenemyanimations.SetBool("walk", true);
            enemy.SetDestination(movingpoint.transform.position);

            ///////////////////////////////////// PLAYER 1
                if (distancetoplayer < 5 && playertopoint < 10)
                {
                    if (playerdead == true)
                    {
                        healthbarbillboard.NotinBattle();//Y
                        farmenemyanimations.SetBool("walk", true);
                        enemy.SetDestination(movingpoint.transform.position);
                    }
                    if (playerdead == false)
                    {
                        healthbarbillboard.Battle();//Y
                        farmenemyanimations.SetBool("walk", true);
                        enemy.SetDestination(player.transform.position);
                        farmenemyanimations.transform.LookAt(player.transform.position);
                    }
                }
                if (playertopoint >= 10)
                {
                    healthbarbillboard.NotinBattle();//Y
                    enemy.SetDestination(movingpoint.transform.position);
                }
                if (distancetoplayer <= 1.4f)
                {
                    healthbarbillboard.Battle();//Y
                    if (attack1 == true)
                    {
                        StartCoroutine(PerformAttack1());
                    }
                    if (attack2 == true)
                    {
                        StartCoroutine(PerformAttack2());
                    }
                }
                if (distancetoplayer > 1.4f)
                {
                    farmenemyanimations.SetBool("attack1", false);
                    farmenemyanimations.SetBool("attack2", false);
                    enemy.GetComponent<NavMeshAgent>().isStopped = false;
                }

            if (distancetopoint < 0.5f)
            {
                farmenemyhp = 40;
                healthbar.SetHealth(farmenemyhp);//Y
                farmenemyanimations.SetBool("walk", false);
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
                            movingpointtrans.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 8);
                            enemy.SetDestination(movingpoint.transform.position);
                            farmenemyanimations.SetBool("walk", true);
                        }
                    }
                    if (backward == true)
                    {
                        if (timer > randomwaittime)
                        {
                            timer = 0;
                            forward = true;
                            backward = false;
                            movingpointtrans.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8);
                            enemy.SetDestination(movingpoint.transform.position);
                            farmenemyanimations.SetBool("walk", true);
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
                            movingpointtrans.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
                            enemy.SetDestination(movingpoint.transform.position);
                            farmenemyanimations.SetBool("walk", true);
                        }
                    }
                    if (backward == true)
                    {
                        if (timer > randomwaittime)
                        {
                            timer = 0;
                            forward = true;
                            backward = false;
                            movingpointtrans.position = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);
                            enemy.SetDestination(movingpoint.transform.position);
                            farmenemyanimations.SetBool("walk", true);
                        }
                    }
                }
            }

            if (PlayerType.rogue == true)
            {
                if (AISCENEPLAYERDAGGERS.daggerplayerhealth <= 0)
                {
                    playerdead = true;
                    enemy.GetComponent<NavMeshAgent>().isStopped = false;
                    enemy.SetDestination(movingpoint.transform.position);
                    farmenemyanimations.SetBool("attack1", false);
                    farmenemyanimations.SetBool("attack2", false);
                    enemy.speed = 2f;
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
                    enemy.GetComponent<NavMeshAgent>().isStopped = false;
                    enemy.SetDestination(movingpoint.transform.position);
                    farmenemyanimations.SetBool("attack1", false);
                    farmenemyanimations.SetBool("attack2", false);
                    enemy.speed = 2f;
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
                    enemy.GetComponent<NavMeshAgent>().isStopped = false;
                    enemy.SetDestination(movingpoint.transform.position);
                    farmenemyanimations.SetBool("attack1", false);
                    farmenemyanimations.SetBool("attack2", false);
                    enemy.speed = 2f;
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
