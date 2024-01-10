using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerStats
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackCooldown;
    [SerializeField]
    private float _powerAttackCooldown;
    [SerializeField]
    private float _attackRange = 2;
    [SerializeField]
    private Animator _animatorController;
    #endregion

    private float _lastAttackTime = 0;

    private float _timeFromLastHit = 0;

    private bool _isDead = false;

    private bool _isEnemyClose = false;

    private Enemie _closestEnemie;

    public delegate void StatsDelegate(int healAmount);

    public StatsDelegate OnEnemyDied;

    public float Hp { get => _hp; }
    public float Damage { get => _damage; }
    public float AttackCooldown { get => _attackCooldown; }
    public float PowerAttackCooldown { get => _powerAttackCooldown; }
    public float AttackRange { get => _attackRange; }
    public float TimeFromLastHit { get => _timeFromLastHit; }
    public bool IsDead { get => _isDead; }
    public bool IsEnemyClose { get => _isEnemyClose; }
    public Enemie ClosestEnemie { get => _closestEnemie; }

    private void Start()
    {
        OnEnemyDied += Heal;
    }

    private void Update()
    {
        if (!_isDead)
        {
            _timeFromLastHit = Time.time - _lastAttackTime;
            _closestEnemie = FindClosestEnemie();
            _isEnemyClose = IsEnemyReached(ClosestEnemie);
            if (Input.GetMouseButtonDown(0) && _timeFromLastHit > _attackCooldown && _isEnemyClose)
            {
                Attack(_closestEnemie);
            }
            if (Input.GetMouseButtonDown(1) && _timeFromLastHit > _powerAttackCooldown && _isEnemyClose)
            {
                StrongAttack(_closestEnemie);
            }
        }
    }

    private void Die()
    {
        _isDead = true;
        _animatorController.SetTrigger("Die");

        SceneManager.Instance.GameOver();
    }

    private void Attack(Enemie enemie)
    {
        _animatorController.SetTrigger("Attack");
        _lastAttackTime = Time.time;

        if (enemie != null)
        {
            transform.transform.rotation = Quaternion.LookRotation(enemie.transform.position - transform.position);

            enemie.TakeDamage(_damage);
        }
    }

    private void StrongAttack(Enemie enemie)
    {
        if (enemie != null)
        {
            _animatorController.SetTrigger("PowerAttack");
            _lastAttackTime = Time.time;
            transform.transform.rotation = Quaternion.LookRotation(enemie.transform.position - transform.position);

            enemie.TakeDamage(_damage);
            enemie.TakeDamage(_damage);
        }
    }

    private bool IsEnemyReached(Enemie enemie)
    {
        var distance = Vector3.Distance(transform.position, enemie.transform.position);
        if (distance <= _attackRange)
            return true;
        return false;
    }

    private Enemie FindClosestEnemie()
    {
        var enemies = SceneManager.Instance.Enemies;
        Enemie closestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
                continue;

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
                closestEnemie = enemie;
        }
        return closestEnemie;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage; 

        if (_hp <= 0 && !_isDead)
        {
            Die();
            
            return;
        }
    }

    private void Heal(int hpAmount) 
    {
        _hp += hpAmount;
    }
}
