using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    
    private Vector2 _mov;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _rb.linearVelocity = _mov.normalized * speed;

        if (_mov.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_mov.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _mov = context.ReadValue<Vector2>();
    }

}
