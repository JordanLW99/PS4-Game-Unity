using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordHPBar : MonoBehaviour
{
    public HealthBar healthbar;
    public Text healthtext;
    int maxhealth;
    int currenthealth;

    // Start is called before the first frame update
    void Start()
    {
        //maxhealth = 20;
        //currenthealth = maxhealth;
        //healthtext.text = currenthealth + "/" + maxhealth;
        //healthbar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<HealthBar>();
        healthtext = GameObject.FindGameObjectWithTag("HPBarText").GetComponent<Text>();

        healthtext.text = AISCENEPLAYER.health + "/" + "100";
        //healthtext.text = AISCENEPLAYERDAGGERS.daggerplayerhealth + "/" + "100";
        healthbar.SetMaxHealth(100);
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    currenthealth -= 1;
        //    healthtext.text = currenthealth + "/" + maxhealth;
        //healthbar.SetHealth(AISCENEPLAYERDAGGERS.daggerplayerhealth);
        healthbar.SetHealth(AISCENEPLAYER.health);
        //}
    }

    public void Heal(int healmount)
    {
        AISCENEPLAYER.health += healmount;
        if (AISCENEPLAYER.health > 100)
        {
            AISCENEPLAYER.health = 100;
        }
        healthbar.SetHealth(AISCENEPLAYER.health);
    }
}
