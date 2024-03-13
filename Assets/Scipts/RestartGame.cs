using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Save previous scene
    // change to that scene
    // set state to play
    public void RestartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel",0));
    }
}
