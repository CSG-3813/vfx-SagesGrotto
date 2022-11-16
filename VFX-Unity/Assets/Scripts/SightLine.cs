using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class SightLine : MonoBehaviour
{
    public Transform EyePoint;
    public string TargetTag = "Player";
    public float FieldOfView = 45f;

    public bool IsTargetInSightLine {get; set; } = false;
    public Vector3 LastKnownSighting {get; set;} = Vector3.zero;

    private SphereCollider ThisCollider;

    private void Awake()
    {
        ThisCollider = GetComponent<SphereCollider>();
        LastKnownSighting = transform.position;
    }

    private bool TargetInFOV(Transform target)
    {
        Vector3 DirToTarget = target.position - EyePoint.position;
        float angle = Vector3.Angle(EyePoint.forward, DirToTarget);

        if(angle <= FieldOfView)
        {
            return true;
        }
        return false;
    }

    private bool HasClearLineofSightToTarget(Transform target)
    {
        RaycastHit info;
        Vector3 DirToTarget = ((target.position + Vector3.up) - EyePoint.position).normalized;

        if (Physics.Raycast(EyePoint.position, DirToTarget, out info, ThisCollider.radius))
        {
            Debug.DrawRay(EyePoint.position, DirToTarget);
            if (info.transform.CompareTag(TargetTag))
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateSight(Transform target)
    {
        IsTargetInSightLine = TargetInFOV(target) && HasClearLineofSightToTarget(target);
        if (IsTargetInSightLine)
        {
            LastKnownSighting = target.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TargetTag))
        {
            UpdateSight(other.transform);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TargetTag))
        {
            IsTargetInSightLine = false;
        }
    }

}

