using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText CombatTextPrefab;

    private float hp;
    private float maxHp;
    private string currentAnimName;
   public bool IsDead => hp <= 0;

    private void Start()
    {
        maxHp = 100;
        OnInit();
        
    }

    private void FixedUpdate()
    {
        HealHp();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);

    }


    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if(!IsDead)
        {
            hp -= damage;
            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHp(hp);
            Instantiate(CombatTextPrefab, transform.position 
                + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    public void HealHp()
    {
        if (hp < 100)
        {
            hp += 5f * Time.deltaTime;
            Debug.Log(hp);
            if (hp > maxHp)
            {
                hp = 100;
            }
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

}
