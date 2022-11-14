using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]

public class AnimationController : MonoBehaviour
{
    private NavMeshAgent thisNavMeshAgent;
    private Animator thisAnimator;

    public float runVel = 0.1f;
    public string animationRunParameter;
    public string animationSpeedParameter;
    private float maxSpeed;

    private void Awake()
    {
        thisNavMeshAgent = GetComponent<NavMeshAgent>();
        thisAnimator = GetComponent<Animator>();
        maxSpeed = thisNavMeshAgent.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisAnimator.SetBool(animationRunParameter, thisNavMeshAgent.velocity.magnitude > maxSpeed);
        thisAnimator.SetFloat(animationSpeedParameter, thisNavMeshAgent.velocity.magnitude/maxSpeed);
    }
}
