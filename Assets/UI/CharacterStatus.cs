using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;

    [Range(0, 100)] public float health;
    [Range(0, 100)] public float stamina;
    [Range(0, 100)] public float cold;
    [Range(0, 100)] public float hunger;

    public Image HealthBar;
    public Image StaminaBar;
    public Image ColdBar;
    public Image HungerBar;

    void Start()
    {
    }

    void Update()
    {
        HealthBar.fillAmount = health / 100;
        StaminaBar.fillAmount = stamina / 100;
        ColdBar.fillAmount = cold / 100;
        HungerBar.fillAmount = hunger / 100;

        if(hunger >= 0)                         //Utrata punktów g³odu
        {
            hunger -= 0.1f * Time.deltaTime;    //Prêdkoœæ utraty g³odu
        }
        if(hunger <= 50)
        {
            health -= 0.1f * Time.deltaTime;
        }
        if(hunger <= 0)
        {
            health -= 0.6f * Time.deltaTime;
        }

        if (cold >= 0)
        {
            cold -= 1.5f * Time.deltaTime;      //Prêdkoœæ utraty ciep³a
        }
        if (cold <= 50)
        {
            FrostEffect cameraObj = playerCamera.GetComponent<FrostEffect>();
            cameraObj.SetFrost(cold / 100);
        }
        if (cold <= 10)
        {
            health -= 0.6f  * Time.deltaTime;
        }


    }
    void OnDestroy()
    {
    }

    void DecreaseStamina()
    {
        stamina -= 10f * Time.deltaTime; // zmniejsz wartoœæ stamina w czasie rzeczywistym
    }
}
