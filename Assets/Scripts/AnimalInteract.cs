using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalInteract : MonoBehaviour
{
    bool isRotating = false;

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.Rotate(0f, 0f, 180 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRotating = true;
            Debug.Log("Player Entered Interact Area");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRotating = false;
            Debug.Log("Player Exited Interact Area");
        }   
    }
}
