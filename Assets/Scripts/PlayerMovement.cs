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

    void Update()
    {
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

        transform.position += dir.normalized * _movementSpeed * Time.deltaTime;
        Debug.Log(dir.magnitude);
        _animator.SetFloat("Speed", dir.magnitude);
    }
}
