using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    int cur_score = 0;
    int tot_score = 0;
    public Font myFont;
    [HideInInspector]
    public BallController ballController;

    void Start ()
    {
        ballController = FindObjectOfType<BallController>();
     //   tot_score += cur_score;
    }
	
	void Update ()
    {
        // Method to count how many pins have fallen down and destroy them after.
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            if (pin.transform.up.y < 0.5f)
            {
                cur_score++;
                Destroy(pin);
            }
        }

        // End-Condition
        if (ballController.ballThrow == 20)
        {
            Application.LoadLevel("game_over");
            // hier de eind score weergeven van de speler
        }
    }

    void OnGUI()
    {
        // GUI to display the score and throwcount
        GUILayout.BeginArea(new Rect(5, 5, 250, 250));

        GUIStyle myBoxStyle = new GUIStyle(GUI.skin.box);
        myBoxStyle.font = myFont;
        myBoxStyle.fontSize = 40;
        myBoxStyle.normal.textColor = Color.white;

        GUILayout.Box("Score : " + cur_score, myBoxStyle);
        GUILayout.Box("Throw : " + ballController.ballThrow, myBoxStyle);

        GUILayout.EndArea();
    }
}
