using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public enum Rarity { Rare, Epic, Legendary };

    private Transform _player;
    public string namePet = string.Empty;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _minDis = 2f;
    private Rigidbody2D _rb;
    public SpriteRenderer sprite;
    private Vector3 dir;
    public Rarity rarity;
    private Animator animator;
    public RuntimeAnimatorController animController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = gameObject.AddComponent<Animator>();

        if (animController != null)
            animator.runtimeAnimatorController = animController;

        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = gameObject.AddComponent<Rigidbody2D>();

        _rb.gravityScale = 0;
        _rb.freezeRotation = true; // Важливо, щоб пет не крутився при колізіях

        sprite = GetComponent<SpriteRenderer>();

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            _player = playerGO.transform;
    }

    private void Update()
    {
        if (_player == null) return;

        Vector3 distance = _player.position - transform.position;

        if (distance.magnitude > _minDis)
        {
            // Плавне прискорення/уповільнення
            float t = Mathf.InverseLerp(_minDis, _minDis + 2f, distance.magnitude);
            dir = distance.normalized * t;
        }
        else
        {
            dir = Vector3.zero;
        }

        if (animator != null)
        {
            animator.SetBool("isRun", dir.sqrMagnitude > 0.01f);
        }

        // Поворот спрайта
        if (dir.x < -0.1f)
        {
            sprite.flipX = true;
        }
        else if (dir.x > 0.1f)
        {
            sprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.velocity = dir * _speed;
        }
    }
}