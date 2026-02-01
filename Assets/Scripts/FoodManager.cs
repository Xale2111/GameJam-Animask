using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FoodGame[] foodGamePrefabs;
    [SerializeField] private Sprite[] foodSprites;
    
    [SerializeField] private GameObject foodObjectPrefab;
    
    [SerializeField] private float spawnUpForce = 6f;
    [SerializeField] private float randomRotationRange = 15f;

    [SerializeField] private bool activate;
    
    
    private int _currentFoodIndex = 0;

    private void Start()
    {
        foodGamePrefabs[_currentFoodIndex].Activate();
    }

    public void GiveFood()
    {
        
    }

    private void NextFood()
    {
        gameManager.SetHeldItemID(_currentFoodIndex);
        foodGamePrefabs[_currentFoodIndex].Deactivate();
        _currentFoodIndex++;
    }

    public void ActivateNextSpot()
    {
        if (_currentFoodIndex < foodGamePrefabs.Length)
        {
            foodGamePrefabs[_currentFoodIndex].Activate();
        }
    }

    public void CreateFood(int foodType)
    {
        int idx = (int)foodType;
        
        if (idx >= foodGamePrefabs.Length || foodGamePrefabs[idx] == null)
        {
            Debug.LogError("Missing foodgame.");
            return;
        }
        
        float random = Random.Range(-randomRotationRange, randomRotationRange);
        Quaternion rotation = Quaternion.Euler(0f, 0f, random);

        GameObject newFood = Instantiate(foodObjectPrefab, foodGamePrefabs[idx].transform.position, rotation);
        newFood.GetComponent<SpriteRenderer>().sprite = foodSprites[idx];
        
        Rigidbody2D rb = newFood.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(Vector2.up * spawnUpForce, ForceMode2D.Impulse);
        }
        
        NextFood();
    }

    public int GetCurrentFoodIndex()
    {
        return _currentFoodIndex;
    }
}
