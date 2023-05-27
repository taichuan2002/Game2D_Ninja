using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if(enemy.Target != null)
        {
            // doi huong enemy toi huong player
            enemy.ChangeDiriction(enemy.Target.transform.position.x >
               enemy.transform.position.x);

            if (enemy.IstargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
            
            
        }
        else
        {
            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }

       
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
