using System;
using UnityEngine;

public class FoodGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private GameObject gameplayRoot;

    private bool _playerInside = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameplayRoot.SetActive(false);
    }

    private void Update()
    {
        if (_playerInside)
        {
            Debug.Log("Player in area");
            if (gameManager.GetIsEyesClosed())
            {
                hudManager.StartGame();
            }
            else
            {
                hudManager.CancelGame();
            }
        }
        else
        {
            hudManager.CancelGame();
        }
    }

    public void Activate()
    {
        gameplayRoot.SetActive(true);
    }

    public void Deactivate()
    {
        gameplayRoot.SetActive(false);
    }

    public void PlayerEntered()
    {
        _playerInside = true;
        Debug.Log("Player Entered Game Area");
    }

    public void PlayerExited()
    {
        _playerInside = false;
        Debug.Log("Player Exited Game Area");
    }
    
    public GameManager GetGameManager()
    {
        return gameManager;
    }
}