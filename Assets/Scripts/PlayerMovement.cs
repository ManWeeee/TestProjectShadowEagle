using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 3f;
    [SerializeField]
    private Animator _animator;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if(!_player.IsDead)
            Move();
    }

    private Vector3 GetDirection()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        return input;
    }

    private void Move()
    {
        Vector3 dir = GetDirection();

        if (dir != Vector3.zero)
        {
            Vector3 targetPosition = transform.position + (dir.normalized);
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position  );
            transform.position += dir.normalized * _movementSpeed * Time.deltaTime;
        }

        _animator.SetFloat("Speed", dir.magnitude);
    }
}
