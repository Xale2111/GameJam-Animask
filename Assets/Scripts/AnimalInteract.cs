using Unity.VisualScripting;
using UnityEngine;

public class AnimalInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
        
    }
}
