using Unity.Cinemachine;
using UnityEngine;

public class CenterCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera baseAreaCamera;
    [SerializeField] private CinemachineCamera playerCamera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            baseAreaCamera.Priority = playerCamera.Priority+1;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            baseAreaCamera.Priority = 0;
        }
    }
}
