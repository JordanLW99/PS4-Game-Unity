using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRender : MonoBehaviour
{
    public GameObject Player, daggerplayer, swordplayer, hammerplayer;
    public SkinnedMeshRenderer skinnedmesh;
    // Start is called before the first frame update
    void Start()
    {
        skinnedmesh = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerType.rogue == true)
        {
            Player = daggerplayer;
        }

        if (PlayerType.warrior == true)
        {
            Player = swordplayer;
        }

        if (PlayerType.hammer == true)
        {
            Player = hammerplayer;
        }

        float distancetoplayer = Vector3.Distance(Player.transform.position, transform.position);

        if (distancetoplayer > 75)
        {
            skinnedmesh.enabled = false;
        }
        if (distancetoplayer < 75)
        {
            skinnedmesh.enabled = true;
        }
    }
}
