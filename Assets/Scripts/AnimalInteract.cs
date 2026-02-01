using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalInteract : MonoBehaviour
{
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
