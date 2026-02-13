using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PetController : MonoBehaviour
{
    public enum Rarity { Rare, Epic, Legendary};

    private Transform _player;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _minDis = 2f;
    private Rigidbody2D _rb;
    public SpriteRenderer sprite;
    private Vector3 dir;
    public Rarity rarity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = gameObject.AddComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
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
            sprite.flipX = true;
        }
        else if (dir.x > 0)
        {
            sprite.flipX = false;
        }


    }
    private void FixedUpdate()
    {
        _rb.velocity = dir * _speed;
    }
}
