using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (FindObjectsOfType<SceneController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GoToScenarioOne()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToScenarioTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(3);
    }

}
