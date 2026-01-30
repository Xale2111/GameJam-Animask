using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    
    private float _xMov;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _rb.linearVelocity = new Vector2(_xMov * speed, _rb.linearVelocity.y);

        if (_xMov < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_xMov > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _xMov = context.ReadValue<Vector2>().x;
    }

}
