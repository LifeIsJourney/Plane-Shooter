using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonClick : MonoBehaviour {
    public static bool hard;
    public int level;
    SecMenuScene slidingMenuObj;
    public Text hardText;
    

    private void Start()
    {
        hard = false;
        Debug.Log("StartonButton start");
        slidingMenuObj = FindObjectOfType<SecMenuScene>();
        
    }
    public void OnbuttonClick()
    {
        Debug.Log("onButtonCLick");
        level = slidingMenuObj.selectedPanID;
        SceneManager.LoadScene(level+1);

    }


    public void OnToggelClick(Toggle t)
    {
        Debug.Log("StartonOnToggelClic");

        if (t.isOn)
        {

            hardText.text = "Easy";
        }
        else
        {
            hard = true;
            hardText.text = "Hard";
        }

        Debug.Log("hard "+hard);
    }
    
   
}
