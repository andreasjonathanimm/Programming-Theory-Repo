using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuIndex);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(mainIndex);
    }

    public void Exit()
    {
        // ABSTRACTION
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
