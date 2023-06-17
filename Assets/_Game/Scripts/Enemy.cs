using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    private IState currentState;

    private bool isRight = true;

    private Character target;
    public Character Target => target;

    private void Update()
    {
        if(currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }



    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());
        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }

    
    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        if(currentState != null )
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        if (IstargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }

    }
    public void Moving()
    {
        ChangeAnim("run");

        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f); 
    }
    

    public bool IstargetInRange()
    {
        if(target != null && Vector2.Distance(target.transform.position,
            transform.position) <= attackRange)
        {
            return true;
        }
        else 
        { 
            return false; 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "EnemyWall")
        {
            ChangeDiriction(!isRight);
        }
    }
   

    public void ChangeDiriction(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero)
            : Quaternion.Euler(Vector3.up * 180);
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

}
