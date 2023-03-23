using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreShow : MonoBehaviour
{
    [SerializeField]
    private GameObject shipObj;

    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        ShipSteering ship = shipObj.GetComponent<ShipSteering>();
        int score = ship.getScore();
        scoreText.text = "Score: " + score.ToString() ;
    }
}
