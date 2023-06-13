using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private Vector3 offset;


    private float hp;
    private float maxHp;

    private Transform target;

    private void Start()
    {
        this.maxHp = 100;
        this.hp = 100;
    }
    void FixedUpdate()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount,
            hp / maxHp, Time.deltaTime * 5f);
        transform.position = target.position + offset;
        HealHp();
    }


    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
    }

    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
    public void HealHp()
    {
        if (hp < 100)
        {
            hp += 5f * Time.deltaTime;
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
