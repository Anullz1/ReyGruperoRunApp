using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { set; get; }
    private bool isGameStarted = false;
    private PlayerMotor motor;

    //UI and the Ui Fields

    public Text scoreText, coinText, modifierText;
    public float score, coinScore, modifierScore;

    private void Awake()
    {
        Instance = this;
        UpdateScore();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
        modifierText.text = modifierScore.ToString();
    }
}

