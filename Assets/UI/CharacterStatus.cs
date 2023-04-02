using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    [Range(0, 100)] public float Health;
    [Range(0, 100)] public float Stamina;
    [Range(0, 100)] public float Cold;
    [Range(0, 100)] public float Hunger;

    public Image HealthBar;
    public Image StaminaBar;
    public Image ColdBar;
    public Image HungerBar;

    void Start()
    {
    }

    void Update()
    {
        HealthBar.fillAmount = Health / 100;
        StaminaBar.fillAmount = Stamina / 100;
        ColdBar.fillAmount = Cold / 100;
        HungerBar.fillAmount = Hunger / 100;

        Hunger -= 0.1f * Time.deltaTime;   //Prêdkoœæ utraty g³odu
        Cold -= 0.4f * Time.deltaTime;      //Prêdkoœæ utraty ciep³a

        //TODO efekt czêœciowej utraty g³odu (wolniejsze poruszanie siê)
        if(Hunger <= 50)
        {
            Health -= 0.1f * Time.deltaTime;
        }
        if(Hunger <= 0)
        {
            Health -= 0.2f * Time.deltaTime;
        }

        //TODO efekt czêœciowej utraty ciep³a (ograniczenie widzenia)
        if (Cold <= 50)
        {
            Health -= 0.1f * Time.deltaTime;
        }
        if (Cold <= 0)
        {
            Health -= 0.2f * Time.deltaTime;
        }


    }
    void OnDestroy()
    {
    }

    void DecreaseStamina()
    {
        Stamina -= 10f * Time.deltaTime; // zmniejsz wartoœæ stamina w czasie rzeczywistym
    }
}
