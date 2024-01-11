using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public abstract class Enemie : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected int _playerHealHpAmount;
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _attackSpeed;
    [SerializeField]
    protected float _attackRange = 2;

    [SerializeField]
    protected Animator AnimatorController;
    [SerializeField]
    protected NavMeshAgent Agent;

    protected float lastAttackTime = 0;
    protected bool isDead = false;

    public abstract void TakeDamage(float _damage);

    protected abstract void Die();
}
