using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public Animator sceneAnimator;
    public int sceneNoToLoad;
    public void Update()
    {   if(Input.GetKeyDown(KeyCode.Space))
        {
            fadeToLevel(1);
        }
    }
    void fadeToLevel(int levelToLoad)
    {
        sceneNoToLoad = levelToLoad;
        sceneAnimator.SetTrigger("fadeOut");
    }
    void OnAnimationComplete()
    {
        FadeToNextLevel();
    }
    void FadeToNextLevel()
    {
        SceneManager.LoadScene(sceneNoToLoad);
    }

}
