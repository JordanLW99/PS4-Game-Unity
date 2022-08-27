using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ArcherEnemy : MonoBehaviour
{
    public GameObject Player, swordplayer, daggerplayer, Arrow, CloneArrow, shootpoint, Bow, hammerplayer;
    public NavMeshAgent Archer;
    public Animator archeranimations;
    public bool canshoot, playerisalive;
    public IEnumerator shooting;
    public Vector3 difference, direction;
    public float distance, rotationZ;
    public int archerhp = 70;
    public int enemygolddrop;
    public bool isdead = false;

    public HealthBar healthbar;//Y
    public Billboard healthbarbillboard;//Y
    public int Maxhp;//Y
    public GameObject losehealthtextprefab;//Y
    public GameObject pos;//Y



    // Start is called before the first frame update
    void Start()
    {
        archerhp = 70;
        Maxhp = archerhp;//Y
        healthbar.SetMaxHealth(Maxhp);//Y
        playerisalive = true;
        shooting = Shoot();
        archeranimations = GetComponent<Animator>();
        Archer = GetComponent<NavMeshAgent>();
    }

    void FireArrow()
    {
        CloneArrow = Instantiate(Arrow, new Vector3(shootpoint.transform.position.x, shootpoint.transform.position.y, shootpoint.transform.position.z), Quaternion.Euler(0.0f, 0.0f, rotationZ)) as GameObject;
        CloneArrow.GetComponent<Rigidbody>().velocity = (direction * 20);
    }

    IEnumerator Shoot()
    {
        canshoot = true;
        yield return new WaitForSeconds(2);
        FireArrow();
        yield return new WaitForSeconds(1.55f);
        canshoot = false;
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
            archerhp = archerhp - AISCENEPLAYER.damage;
            Debug.Log(AISCENEPLAYER.damage);
            healthbar.SetHealth(archerhp);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + AISCENEPLAYER.damage;//Y
            if (archerhp <= 0)
            {
                Archer.GetComponent<NavMeshAgent>().isStopped = true;
                archeranimations.SetBool("death", true);
                isdead = true;
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(6, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(3, 7);
                }
                    AISCENEPLAYER.gold = AISCENEPLAYER.gold + enemygolddrop;
                Debug.Log("You now have: " + AISCENEPLAYER.gold + " coins");
            }
        }

        if (other.gameObject.tag == "PlayerDagger" && AISCENEPLAYERDAGGERS.daggerattacking == true)
        {
            archerhp = archerhp - AISCENEPLAYERDAGGERS.daggerdamage;
            healthbar.SetHealth(archerhp);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + AISCENEPLAYERDAGGERS.daggerdamage;//Y
            Debug.Log("Player Damage " + AISCENEPLAYERDAGGERS.daggerdamage);
            if (archerhp <= 0)
            {
                Archer.GetComponent<NavMeshAgent>().isStopped = true;
                archeranimations.SetBool("death", true);
                isdead = true;
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(6, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(3, 7);
                }
                AISCENEPLAYERDAGGERS.daggergold = AISCENEPLAYERDAGGERS.daggergold + enemygolddrop;
                Debug.Log("You now have: " + AISCENEPLAYERDAGGERS.daggergold + " coins");
            }
        }

        if (other.gameObject.tag == "PlayerHammer" && HammerPlayer.hammerattacking == true)
        {
            archerhp = archerhp - HammerPlayer.hammerdamage;
            healthbar.SetHealth(archerhp);//Y
            GameObject abc = Instantiate(losehealthtextprefab, this.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, pos.transform);//Y
            abc.GetComponent<Text>().text = "-" + HammerPlayer.hammerdamage;//Y
            Debug.Log("Player Damage " + HammerPlayer.hammerdamage);
            if (archerhp <= 0)
            {
                Archer.GetComponent<NavMeshAgent>().isStopped = true;
                archeranimations.SetBool("death", true);
                isdead = true;
                if (Buff.golddropbuffon == true)
                {
                    enemygolddrop = Random.Range(6, 11);
                }
                else
                {
                    enemygolddrop = Random.Range(3, 7);
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
            Player = swordplayer;
        }

        if (PlayerType.rogue == true)
        {
            Player = daggerplayer;
        }

        if (PlayerType.hammer == true)
        {
            Player = hammerplayer;
        }

        if (isdead == false)
        {
            difference = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z) - transform.position;
            distance = difference.magnitude;
            direction = difference / distance;
            direction.Normalize();
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            float distancetoplayer = Vector3.Distance(Player.transform.position, transform.position);

            if (distancetoplayer < 20 && playerisalive)
            {
                healthbarbillboard.Battle();//Y
                shootpoint.transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z));
                Archer.transform.LookAt(new Vector3(Player.transform.position.x, Archer.transform.position.y, Player.transform.position.z));
                archeranimations.SetBool("Shoot", true);
                if (canshoot == false)
                {
                    StartCoroutine(Shoot());
                }
            }

            if (distancetoplayer > 20)
            {
                healthbarbillboard.NotinBattle();//Y
                archeranimations.SetBool("Shoot", false);
            }

            if (AISCENEPLAYER.health > 0)
            {
                playerisalive = true;
            }

            if (AISCENEPLAYER.health <= 0)
            {
                playerisalive = false;
                archeranimations.SetBool("Shoot", false);
                healthbarbillboard.NotinBattle();//Y
            }

            if (AISCENEPLAYERDAGGERS.daggerplayerhealth > 0)
            {
                playerisalive = true;
            }

            if (AISCENEPLAYERDAGGERS.daggerplayerhealth <= 0)
            {
                playerisalive = false;
                archeranimations.SetBool("Shoot", false);
                healthbarbillboard.NotinBattle();//Y
            }

            if (HammerPlayer.hammerhealth > 0)
            {
                playerisalive = true;
            }

            if (HammerPlayer.hammerhealth <= 0)
            {
                playerisalive = false;
                archeranimations.SetBool("Shoot", false);
                healthbarbillboard.NotinBattle();//Y
            }
        }

        if(isdead == true)
        {
            StartCoroutine(Die());
        }
    }
}
