using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuController : MonoBehaviour
{
    public void StartButtonClicked()
    {
        Application.LoadLevel("main");
    }
    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}
