using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerManage : MonoBehaviour
{
    [SerializeField] public int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
