using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string level;
    public void StartGame()
    {
        SceneManager.LoadScene(level);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
