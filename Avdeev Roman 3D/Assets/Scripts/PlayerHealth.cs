using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthObject,ITakeDamage
{
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    //для отладки нанесение урона себе по ПКМ
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TakeDamage(4);
        }
    }
}
