using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject player, swordplayer, daggerplayer, hammerplayer;
    public Animator ShopKeeperAnimations;
    public static bool caninteract = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ShopKeeperAnimations = GetComponent<Animator>();
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

        float distancetoplayer = Vector3.Distance(player.transform.position, transform.position);

        if(distancetoplayer < 5)
        {
            caninteract = true;
            ShopKeeperAnimations.SetBool("Wave", true);
        }
        if (distancetoplayer > 5)
        {
            caninteract = false;
            ShopKeeperAnimations.SetBool("Wave", false);
        }
    }
}
