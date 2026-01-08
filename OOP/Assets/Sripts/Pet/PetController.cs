using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _minDis = 2f;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Vector3 dir;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 distance = _player.position - transform.position;
        if (distance.magnitude > _minDis) {
            float t = Mathf.InverseLerp(_minDis, _minDis + 2f, distance.magnitude);
            dir = distance.normalized * t;
        }
        else
        {
            dir = Vector3.zero;
        }
        if (dir.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (dir.x > 0)
        {
            _sprite.flipX = false;
        }


    }
    private void FixedUpdate()
    {
        _rb.velocity = dir * _speed;
    }
}
