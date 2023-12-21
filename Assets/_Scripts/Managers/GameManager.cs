using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private BaseState currentState;
    public BaseState CurrentState
    {
        get { return currentState; }
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start() => SetState(typeof(StartState));

    //Changes the current game state
    public void SetState(System.Type newStateType)
    {
        currentState?.OnDeactivate();

        currentState = GetComponentInChildren(newStateType) as BaseState;

        currentState?.OnActivate();
    }


    public void ButtonClick(string str)
    {
        switch(str)
        {
            case "Restart":
                SceneManager.LoadScene(0);
                break;
        }
    }
}