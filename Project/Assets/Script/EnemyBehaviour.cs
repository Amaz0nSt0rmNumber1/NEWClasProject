using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform Moon;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Moon = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();

        }
    }

    void InitalizePatrolRoute()
    {
        foreach (Transform child in locations)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Moon")

        {
            agent.destination = Moon.position;
            Debug.Log("Moon detected - ATTACK!!!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Moon")
        {
         Debug.Log("Moon out of range, resume patrol.");
        }
    }
}