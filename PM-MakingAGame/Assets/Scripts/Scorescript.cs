using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorescript : MonoBehaviour
{
    public static int ScoreValue = 0;
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (DifficultySetting.difficultyMode)
        {
            case Diff.Ez:
                score.text = "Score: " + ScoreValue;
                break;
            case Diff.Norm:
                score.text = "Score: " + ScoreValue*2;
                break;
            case Diff.Hard:
                score.text = "Score: " + ScoreValue*3;
                break;

            default:
                break;
        }
        
    }
    
}
