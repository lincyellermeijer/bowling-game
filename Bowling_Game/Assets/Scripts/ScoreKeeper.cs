using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreKeeper : MonoBehaviour
{
    public List<BowlingFrame> Frames = new List<BowlingFrame>();
    public int frame;
    public int pinsDown;
    public int frameBall = 0;
    public static int score;
    public bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        Frames.Add(new BowlingFrame(0));
    }

    // If frames > 20 then the game is finished, go to game over screen
    public void Update()
    {
        if (frame > 20)
        {
            gameOver = true;
            Application.LoadLevel("game_over");
        }
    }

    // Method to count how many pins have fallen down and destroy them after.
    int getDownPins()
    {
        int pinsDown = 0;
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            if (pin.transform.up.y < 0.5f)
            {
                pinsDown += 1;
            }
        }
        return pinsDown;
    }

    // Update the score after each frame
    public void UpdateScore(object ballThrow)
    {
        pinsDown = getDownPins();
        BowlingFrame bowlingFrame = Frames[Frames.Count - 1].AddScore(frameBall, pinsDown);
        frameBall += 1;
        if (bowlingFrame != null)
        {
            Frames.Add(bowlingFrame);
            NewFrame();
        }
    }

    // Reset frame after each frame has ended
    public void NewFrame()
    {
        frameBall = 0;
        frame = Frames.Count;
        Debug.Log("Starting frame " + frame.ToString());
        gameObject.SendMessage("ResetFrame", SendMessageOptions.RequireReceiver);
        score = BowlingFrame.Score(Frames);
    }

}

// Class for all the information about the bowlingFrame
public class BowlingFrame
{
    int Score1 = 0;
    int Score2 = 0;
    int Carry;

    // Temporary 'storage' for storing the data of each frame (used to calculate score after each frame)
    public BowlingFrame(int carries)
    {
        Carry = carries;
    }

    // Calculates score 1 and score 2 after a ballThrow
    public BowlingFrame AddScore(int ball, int score)
    {
        if (ball == 0)
        {
            Score1 = Mathf.Max(score, 0);
            if (score == 10)
            {
                return new BowlingFrame(2);
            }
            return null;
        }
        else
        {
            Score2 = score - Score1;
            if (Score1 + Score2 == 10)
                return new BowlingFrame(1);
            return new BowlingFrame(0);
        }
    }

    // Calculates the total score of 1 and 2 after the frame has ended.
    // Checks this for all the frames in de list BowlingFrame with an IEnumerable
    public static int Score(IEnumerable<BowlingFrame> frames)
    {
        int score = 0;
        foreach (BowlingFrame f in frames)
        {
            score += f.Score1;
            score += f.Score2;
            if (f.Carry > 0) score += f.Score1;
            if (f.Carry > 1) score += f.Score2;
        }
        return score;
    }
}