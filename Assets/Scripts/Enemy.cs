using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected override void Die()
    {
        Destroy(gameObject);
        // Implement enemy-specific death behavior here
    }
}
