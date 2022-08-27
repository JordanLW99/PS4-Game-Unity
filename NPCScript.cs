using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    public NavMeshAgent NPCNavMesh;
    public Animator NPCAnimator;
    public int i = 0;
    public int j = 0;
    public int k = 0;
    private Nodes nodes;
    public bool npcpath1 = false;
    public bool npcpath2 = false;
    public bool npcpath3 = false;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NPCAnimator = GetComponent<Animator>();
        NPCNavMesh = GetComponent<NavMeshAgent>();
        nodes = FindObjectOfType<Nodes>();
        nodes.Path1 = GameObject.FindGameObjectsWithTag("Path1");
        nodes.Path2 = GameObject.FindGameObjectsWithTag("Path2");
        nodes.Path3 = GameObject.FindGameObjectsWithTag("Path3");
        i = Random.Range(20, 50);
        j = Random.Range(0, nodes.Path2.Length);
        k = Random.Range(0, nodes.Path3.Length);
    }

    private void Moving()
    {
        float distancetopath1 = Vector3.Distance(nodes.Path1[i].transform.position, transform.position);
        float distancetopath2 = Vector3.Distance(nodes.Path2[j].transform.position, transform.position);
        float distancetopath3 = Vector3.Distance(nodes.Path3[k].transform.position, transform.position);

        //path 1
        if (npcpath1 == true)
        {
            if (i <= nodes.Path1.Length)
            {
                NPCNavMesh.destination = nodes.Path1[i].transform.position;
            }
            if (distancetopath1 < 0.5f)
            {
                i++;
            }
            if (i == nodes.Path1.Length)
            {
                i = 0;
            }
        }

        //path 2
        if (npcpath2 == true)
        {
            if (j <= nodes.Path2.Length)
            {
                NPCNavMesh.destination = nodes.Path2[j].transform.position;
            }
            if (distancetopath2 < 0.5f)
            {
                j++;
            }
            if (j == nodes.Path2.Length)
            {
                j = 0;
            }
        }

        //path 3
        if (npcpath3 == true)
        {
            if (k <= nodes.Path3.Length)
            {
                NPCNavMesh.destination = nodes.Path3[k].transform.position;
            }
            if (distancetopath3 < 0.5f)
            {
                k++;
            }
            if (k == nodes.Path3.Length)
            {
                k = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }
}
