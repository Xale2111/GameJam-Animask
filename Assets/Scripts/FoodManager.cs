using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodManager : MonoBehaviour
{
    
    
    [SerializeField] private GameObject[] foodGamePrefab;
    [SerializeField] private GameObject foodObjectPrefab;
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
            activate = false;
        }
    }

    void CreateFood()
    {
        float random = Random.Range(-15f, 15f);

        Quaternion rotation = Quaternion.Euler(0f, 0f, random);

        GameObject newFood = Instantiate(foodObjectPrefab, foodGamePrefab[currentFoodIndex % foodGamePrefab.Length].transform.position, rotation);
        newFood.GetComponent<SpriteRenderer>().sprite = foodSprites[currentFoodIndex % foodSprites.Length];
        currentFoodIndex = currentFoodIndex + 1;
        
        Rigidbody2D rb = newFood.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(Vector2.up * spawnUpForce, ForceMode2D.Impulse);
        }
    }
}
