using UnityEngine;

public class FoodGameTrigger : MonoBehaviour
{
    private FoodGame _parentGame;
    private GameManager _gameManager;

    private void Awake()
    {
        _parentGame = GetComponentInParent<FoodGame>();
        _gameManager = _parentGame.GetGameManager();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _parentGame.PlayerEntered();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _parentGame.PlayerExited();
    }
}