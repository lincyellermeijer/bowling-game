using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

    public Text scoreText;
    public Font myFont;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "";
    }

    void Update()
    {
        Debug.Log("Display score");
        scoreText.text = ("Total Score : " + ScoreKeeper.score);
    }
}
