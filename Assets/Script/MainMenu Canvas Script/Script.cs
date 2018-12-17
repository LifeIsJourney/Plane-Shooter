using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script : MonoBehaviour {
    public Dropdown myDropdown;
    public static string gamePlay;
    int sceneId = 0;
 //  public static bool Easy
	public void OnclickOne()
    {
        
        //SceneManager.LoadScene(1);
    }public void OnclickTwo()
    {
        SceneManager.LoadScene(2);
    }
    private void Start()

    {
        switch (myDropdown.value)
        {
            case 1:
                Debug.Log("Dropdown 1");
                gamePlay = "easy";
                break;
            case 2:
                Debug.Log("Dropdown 2");
                gamePlay = "hard";
                break;

        }


    }
    public  void forSecondMenu()
    {
        sceneId = FindObjectOfType<SecMenuScene>().selectedPanID;

        {
            Debug.Log(sceneId);
           // SceneManager.LoadScene(sceneId=+1);
            switch (myDropdown.value)
            {
                case 1:
                    Debug.Log("Dropdown 1");
                    gamePlay = "easy";
                    break;
                case 2:
                    Debug.Log("Dropdown 2");
                    gamePlay = "hard";
                    break;

            }


        }

    }
    private void Update()
    {
       
    }
}
