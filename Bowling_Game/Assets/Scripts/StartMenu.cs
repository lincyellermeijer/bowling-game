using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    // If start button is clicked, go to the main level
    public void StartButtonClicked()
    {
        Application.LoadLevel("main");
        ScoreKeeper.score = 0;
    }

    // If exit button is clicked, quit the game. (only works in .exe build!)
    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}
