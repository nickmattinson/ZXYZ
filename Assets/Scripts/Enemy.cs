using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Die()
    {
        Destroy(gameObject);
        // Implement enemy-specific death behavior here
        Debug.Log("Enemy died!");
    }
}
