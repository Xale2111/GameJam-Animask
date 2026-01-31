using System;
using UnityEngine;


public enum ArrowDirection { Left, Down, Up, Right, none}

public class Arrow : MonoBehaviour
{
  [SerializeField] private float speed = 150f;
  
  ArrowDirection _direction;
  

  private void Update()
  {
    transform.Translate(Vector2.left * speed * Time.deltaTime);
  }

  public void SetDirection(ArrowDirection direction)
  {
    _direction = direction;
  }
  
  
  public ArrowDirection GetDirection()
  {
    return _direction;
  }
}
