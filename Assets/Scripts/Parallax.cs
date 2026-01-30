using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _startPos, length;
    public GameObject cam;
    [Range(0.0f, 1.0f)]
    public float parallaxSpeed; //relative to camera, 0 means move with camera, 1 means no movement
    
    void Start()
    {
        _startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxSpeed;
        float movement = cam.transform.position.x * (1 -  parallaxSpeed);
        
        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        if (movement > _startPos + length)
        {
            _startPos += length;
        }
        else if (movement < _startPos - length)
        {
            _startPos -= length;
        }
    }
}
