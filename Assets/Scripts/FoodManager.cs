using System;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    
    public enum Foods { Berry, Honey, Length }
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Sprite[] foodSprites;
    private int currentFoodIndex = 0;
    
    [SerializeField] private float spawnUpForce = 3f;
    [SerializeField] private bool activate;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            CreateFood();
        }   
    }

    void CreateFood()
    {
        GameObject newFood = Instantiate(foodPrefab, spawnPoint.position, foodPrefab.transform.rotation);
        newFood.GetComponent<SpriteRenderer>().sprite = foodSprites[currentFoodIndex];
        currentFoodIndex = (currentFoodIndex + 1) % foodSprites.Length;
        
        Rigidbody2D rb = newFood.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(Vector2.up * spawnUpForce, ForceMode2D.Impulse);
        }

        activate = false;
    }
}
