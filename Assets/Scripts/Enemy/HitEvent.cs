using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    public Enemy Enemy;

    public void PLayerHit()
    {
        Enemy.AttackAction();
    }
}
