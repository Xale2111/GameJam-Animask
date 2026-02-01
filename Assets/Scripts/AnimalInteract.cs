using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalInteract : MonoBehaviour
{
    [SerializeField] private int animalID = 0;
    
    private void Awake()
    {
        if(animalID != 0)
            gameObject.SetActive(false);
    }

    public void SetActiveAnimal()
    {
        gameObject.SetActive(true);
    }

    public int GetAnimalID()
    {
        return animalID;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Interact Area");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exited Interact Area");
        }   
    }
}