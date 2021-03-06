using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Contains indexes for Scenes and allows exiting the Application
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Encapsulation
    private int menuIndex = 0;
    private int mainIndex = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Loads the Menu
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(menuIndex);
    }

    /// <summary>
    /// Loads the Main Scene
    /// </summary>
    public void LoadMain()
    {
        SceneManager.LoadScene(mainIndex);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void Exit()
    {
        // Abstraction
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
