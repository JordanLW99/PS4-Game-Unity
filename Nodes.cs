using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public GameObject[] Path1;
    public GameObject[] Path2;
    public GameObject[] Path3;

    void Start()
    {
        Path1 = GameObject.FindGameObjectsWithTag("Path1");
        Path2 = GameObject.FindGameObjectsWithTag("Path2");
        Path3 = GameObject.FindGameObjectsWithTag("Path3");

    }

    void Update()
    {
      
    }
}
