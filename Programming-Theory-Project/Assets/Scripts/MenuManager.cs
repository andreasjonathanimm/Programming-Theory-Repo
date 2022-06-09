using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void LoadMain()
    {
        gameManager.LoadMain();
    }

    public void Exit()
    {
        gameManager.Exit();
    }
}
