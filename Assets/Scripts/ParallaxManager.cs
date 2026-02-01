using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    private float _startPos, _length;
    public Camera mainCamera;
    [Range(0.0f, 1.0f)]
    public float parallaxSpeed; //relative to camera, 0 means move with camera, 1 means no movement
    
    [SerializeField] bool isFixed = false;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    void Start()
    {
        _startPos = transform.position.x;
        _length = 0.0f;
        if (!isFixed)
        {
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = mainCamera.transform.position.x * parallaxSpeed;
        float movement = mainCamera.transform.position.x * (1 -  parallaxSpeed);
        
        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        if (!isFixed)
        {
            if (movement > _startPos + _length)
            {
                _startPos += _length;
            }
            else if (movement < _startPos - _length)
            {
                _startPos -= _length;
            }
        }
    }
}
