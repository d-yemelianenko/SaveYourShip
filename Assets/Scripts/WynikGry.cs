using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WynikGry : MonoBehaviour
{
    [SerializeField]
    private GameObject shipObj;

    private TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        ShipSteering ship = shipObj.GetComponent<ShipSteering>();
        score = ship.getScore();
        scoreText.text = "Twój wynik: " + score.ToString();
    }
}
