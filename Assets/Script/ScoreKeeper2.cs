using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper2 : MonoBehaviour {
     Text myText;
   public static int score = 0;
   public  int playerHighScore;
	// Use this for initialization
	void Start () {

        myText = GetComponent<Text>();
       
    }
    private void Update()
    {
    }
    public void Score(int point)
    {
       // for (int i = 0; i < point; i++)
        {
          
            score += point;
           
        }
        myText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("PlaneHighScore", 0))
        {
            PlayerPrefs.SetInt("PlaneHighScore",score);
        }
        playerHighScore = PlayerPrefs.GetInt("PlaneHighScore", 0);
        Debug.Log(playerHighScore);

    }
    
    public void Reset()
    {
        score = 0;
        myText.text = score.ToString();
    }
    public  void Bonus(int point)
    {
       // for (int i = 0; i < point; i++)
        {
            Score(point);
        }

    }
}
