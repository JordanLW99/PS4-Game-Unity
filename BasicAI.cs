using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;
using System.Linq.Expressions;
using System;

public class BasicAI : MonoBehaviour
{
    //AI Variables
    public Transform Enemy;
    public GameObject Player;
    Root aiRoot = BT.Root();
    public NavMeshAgent enemyagent;
    public Transform playerpos;

    //chase function
    public bool visibleplayer;
    public bool invisibleplayer;

    //walk/patrol/wander function
    public GameObject[] points;
    public int i = 0;
    public float timer;
    public int randomwaittime;


    private void OnEnable()
    {
        points = GameObject.FindGameObjectsWithTag("WanderPoints"); //wander function
        randomwaittime = UnityEngine.Random.Range(1, 5);

        aiRoot.OpenBranch(BT.If(Playerisvisible).OpenBranch(BT.Call(Chase)),BT.If(Playerisnotvisible).OpenBranch(BT.Call(Walk)));
    }

    private void Walk()
    {
        float distancetopoints = Vector3.Distance(points[i].transform.position, transform.position);

        if (distancetopoints < 0.5f)
        {
            timer += Time.deltaTime;
            if (timer > randomwaittime)
            {
                randomwaittime = UnityEngine.Random.Range(1, 7);
                timer = 0;
                i = UnityEngine.Random.Range(0, points.Length);
            }
            if (i < points.Length)
            {
                enemyagent.destination = points[i].transform.position;
            }
        }
        else
        {
            enemyagent.destination = points[i].transform.position;
        }

        if (i == points.Length)
        {
            i = 0;
            enemyagent.destination = points[i].transform.position;
        }
    }

    private void Attack()
    {

    }

    private bool Playerisvisible()
    {
        float distbetweenagents = Vector3.Distance(playerpos.transform.position, transform.position);

       if (distbetweenagents < 10f)
       {
            visibleplayer = true;
            invisibleplayer = false;
       }
        return visibleplayer;
    }

    private bool Playerisnotvisible()
    {
        float distbetweenagents = Vector3.Distance(playerpos.transform.position, transform.position);

        if (distbetweenagents > 10f)
        {
            invisibleplayer = true;
            visibleplayer = false;
        }
        return invisibleplayer;
    }

    private void Chase()
    {
        if(visibleplayer == true)
        {
            //enemyagent.GetComponent<NavMeshAgent>().isStopped = false;
            enemyagent.SetDestination(playerpos.position);
        }
        else
        {
            //enemyagent.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    private void Update()
    {
        Playerisvisible();
        Playerisnotvisible();
        aiRoot.Tick();
    }
    public Root GetAIRoot()
    {
        return aiRoot;
    }
}
