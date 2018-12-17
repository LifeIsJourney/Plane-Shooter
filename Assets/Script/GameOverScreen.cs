using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour {
    public Text playerScore;
    public Text playerHighScore;
    int score ;
    public GameObject particle;
	// Use this for initialization
	void Start () {
        score = ScoreKeeper2.score;
          playerScore.text = score.ToString();
         playerHighScore.text = (PlayerPrefs.GetInt("PlaneHighScore", 0)).ToString();
       // Instantiate(particle, transform.position, Quaternion.identity);
	}
	
  public  void OnBckToMenuClick()
    {
        SceneManager.LoadScene(0);

    }
	// Update is called once per frame
	void Update () {
		
	}
}
