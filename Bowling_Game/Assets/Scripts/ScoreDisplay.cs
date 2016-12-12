using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;
    public Font myFont;

    // GUI to display the score and the number of frames and balls in boxes.
    void OnGUI()
    {
       GUIStyle myBoxStyle = new GUIStyle(GUI.skin.box);
        myBoxStyle.font = myFont;
        myBoxStyle.fontSize = 20;
        myBoxStyle.normal.textColor = Color.white;
        
        Rect r = new Rect(50, 50, 300, 50);
        GUI.Box(r, "Score: " + ScoreKeeper.score.ToString(), myBoxStyle);
        Rect s = new Rect(50, 125, 300, 50);
        string frameball = "Frame: {0}    Ball:{1}";
        GUI.Box(s, string.Format(frameball, scoreKeeper.frame, scoreKeeper.frameBall), myBoxStyle);
    }
}
