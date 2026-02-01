using UnityEngine;

public class BrogleSpin : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool spinRight = true;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime * (spinRight ? 1 : -1));
    }
}
