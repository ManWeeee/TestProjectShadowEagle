using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerStats
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackDelay;
    [SerializeField]
    private float _attackRange = 2;
    [SerializeField]
    private Animator _animatorController;
    #endregion

    public float Hp { get => _hp; }
    public float Damage { get => _damage; }
    public float AttackDelay { get => _attackDelay; }
    public float AttackRange { get => _attackRange; }

    private float lastAttackTime = 0;
    private bool isDead = false;
    
    private void Update()
    {
        Attack();  
    }

    private void Die()
    {
        isDead = true;
        _animatorController.SetTrigger("Die");

        SceneManager.Instance.GameOver();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > _attackDelay) 
        {
            _animatorController.SetTrigger("Attack");
            lastAttackTime = Time.time;

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

            if (closestEnemie != null)
            {
                var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
                if (distance <= _attackRange)
                {
                    //transform.LookAt(closestEnemie.transform);
                    transform.transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - transform.position);

                    closestEnemie.TakeDamage(_damage);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage; 

        if (_hp <= 0)
        {
            Die();
            return;
        }

    }
}
