using Bunny_TK.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AISimpleChase : MonoBehaviour
{
    NavMeshAgent agent;
    float defaultSpeed;
    float defaultAngluarSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed;
        defaultAngluarSpeed = agent.angularSpeed;
    }

    private void Update()
    {
        agent.SetDestination(Player.Instance.transform.position);
        agent.speed = defaultSpeed * TimeManager.Instance.scaler;
        agent.angularSpeed = defaultAngluarSpeed * TimeManager.Instance.scaler;
    }
}
