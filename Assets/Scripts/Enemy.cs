using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform target;

    public float minSpeed;
    public float maxSpeed;

    void Start(){ 
    
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update(){ 
        agent.destination = target.position;
    
    }
}
