using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string LevelName;

    public void GoToLeavel()
    {
        Debug.Log("Error");
        SceneManager.LoadScene(LevelName);
    }
}
