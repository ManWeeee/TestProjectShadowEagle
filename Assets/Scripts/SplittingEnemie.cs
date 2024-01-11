using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class SplittingEnemie : Enemie
{
    [SerializeField]
    private int _splitGoblinAmount;
    [SerializeField]
    private Enemie _enemiePrefab;
    [SerializeField]
    private float _spawnEnemieRange;

    private void Start()
    {
        SceneManager.Instance.AddEnemie(this);
        Agent.SetDestination(SceneManager.Instance.Player.transform.position);

    }

    private void Update()
    {
        if (isDead)
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

    public override void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Die();
            SceneManager.Instance.Player.OnEnemyDied?.Invoke(_playerHealHpAmount);
            return;
        }
    }

    protected override void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");
        for (int i = 0; i < _splitGoblinAmount; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(transform.position.x + _spawnEnemieRange, transform.position.x - _spawnEnemieRange), 0, UnityEngine.Random.Range(transform.position.z + _spawnEnemieRange, transform.position.z - _spawnEnemieRange));
            Instantiate(_enemiePrefab, pos, Quaternion.identity);
        }

        SceneManager.Instance.RemoveEnemie(this);
    }
}
