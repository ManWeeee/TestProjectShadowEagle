using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
    [SerializeField]
    private int _playerHealHpAmount;
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    private float _attackRange = 2;


    public Animator AnimatorController;
    public NavMeshAgent Agent;

    private float lastAttackTime = 0;
    private bool isDead = false;


    private void Start()
    {
        SceneManager.Instance.AddEnemie(this);
        Agent.SetDestination(SceneManager.Instance.Player.transform.position);

    }

    private void Update()
    {
        if(isDead)
        {
            return;
        }

        if (_hp <= 0)
        {
            Die();
            Agent.isStopped = true;
            return;
        }

        var distance = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);
     
        if (distance <= _attackRange)
        {
            Agent.isStopped = true;
            if (Time.time - lastAttackTime > _attackSpeed)
            {
                lastAttackTime = Time.time;
                SceneManager.Instance.Player.TakeDamage(_damage);
                AnimatorController.SetTrigger("Attack");
            }
        }
        else
        {
            Agent.isStopped = false;
            Agent.SetDestination(SceneManager.Instance.Player.transform.position);
        }
        AnimatorController.SetFloat("Speed", Agent.speed); 

    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Die();
            SceneManager.Instance.Player.OnEnemyDied?.Invoke(_playerHealHpAmount);
            return;
        }

    }

    private void Die()
    {
        SceneManager.Instance.RemoveEnemie(this);
        isDead = true;
        AnimatorController.SetTrigger("Die");
    }

}
