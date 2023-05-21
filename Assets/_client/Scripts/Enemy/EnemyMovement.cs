using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [Range(0, 20)] [SerializeField] private float distance = 2.5f;

    private float _startPosition;
    private bool _movingRight = true;

    private void Start()
    {
        _startPosition = gameObject.transform.position.x;
    }

    private void Update()
    {
      
        if (transform.position.x > _startPosition + distance)
        {
            _movingRight = false;
        }
        else if (transform.position.x < _startPosition - distance)
        {
            _movingRight = true;
        }

        if (_movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
}