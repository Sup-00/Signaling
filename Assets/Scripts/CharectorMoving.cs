using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharectorMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private MoveEvent _charectorMoving;
    [SerializeField] private UnityEvent _charectorIdle;

    private Rigidbody2D _rigidbody2D;
    private bool _isStayOnFeet;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0 )
        {
            _charectorMoving.Invoke(true);
            Move(-_speed);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _charectorMoving.Invoke(false);
            Move(_speed);
        }
        else
        {
            _charectorIdle.Invoke();
        }

        if (_isStayOnFeet && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isStayOnFeet = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isStayOnFeet = false;
    }

    private void Move(float MoveDirection)
    {
        transform.Translate(MoveDirection * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}

[System.Serializable]
public class MoveEvent : UnityEvent<bool> { }
