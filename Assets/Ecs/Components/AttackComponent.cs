using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackComponent 
{
    public HealthComponent Target;
    public int Damage;
    public float AttackDelay;
    public float AttackDistance;
}
