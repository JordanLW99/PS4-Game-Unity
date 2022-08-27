using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaggerHPBar : MonoBehaviour
{
    public HealthBar healthbar;
    public Text healthtext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<HealthBar>();
        healthtext = GameObject.FindGameObjectWithTag("HPBarText").GetComponent<Text>();

        healthtext.text = AISCENEPLAYERDAGGERS.daggerplayerhealth + "/" + "100";
        healthbar.SetMaxHealth(100);
    
        healthbar.SetHealth(AISCENEPLAYERDAGGERS.daggerplayerhealth);
    }

    public void Heal(int healmount)
    {
        AISCENEPLAYERDAGGERS.daggerplayerhealth += healmount;
        if (AISCENEPLAYERDAGGERS.daggerplayerhealth > 100)
        {
            AISCENEPLAYERDAGGERS.daggerplayerhealth = 100;
        }
        healthbar.SetHealth(AISCENEPLAYERDAGGERS.daggerplayerhealth);
    }
}
