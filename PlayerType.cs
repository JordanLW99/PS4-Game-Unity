using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    public GameObject Player1, Player2, Player3;
    public static bool choosewarrior, chooserogue, choosehammer;
    public static bool warrior, rogue, hammer;
    // Start is called before the first frame update
    void Start()
    {

        if (choosewarrior == true)
        {
            chooserogue = false;
            choosehammer = false;
            warrior = true;
            rogue = false;
            hammer = false;
            Player1.SetActive(true);
            Player2.SetActive(false);
            Player3.SetActive(false);
            //Instantiate(Player1, new Vector3(274.8f, 50.00305f, 396.4f), Quaternion.Euler(0, 0, 0));
        }
        if (chooserogue == true)
        {
            choosewarrior = false;
            choosehammer = false;
            rogue = true;
            warrior = false;
            hammer = false;
            Player1.SetActive(false);
            Player2.SetActive(true);
            Player3.SetActive(false);
            //Instantiate(Player2, new Vector3(274.8f, 50.00305f, 396.4f), Quaternion.Euler(0, 0, 0));

        }

        if (choosehammer == true)
        {
            choosewarrior = false;
            chooserogue = false;
            hammer = true;
            rogue = false;
            warrior = false;
            Player1.SetActive(false);
            Player2.SetActive(false);
            Player3.SetActive(true);
            //Instantiate(Player2, new Vector3(274.8f, 50.00305f, 396.4f), Quaternion.Euler(0, 0, 0));

        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
