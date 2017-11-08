using UnityEngine;
using System.Collections;

public abstract class FishBowlInfo : MonoBehaviour
{
    protected const float YaxisOffset = 7.5f;
    [SerializeField]
    protected GameObject forceTarget;
    public float forceTargetRadius;
    public bool enableForceTarget;

    protected abstract void CalculateRange();

    public abstract Vector3 GetRandomPosInBowl();

    public abstract Vector3 GetRandomFishTarget(Vector3 original);

    public Vector3 GetForceTargetPos()
    {
        return transform.InverseTransformPoint(forceTarget.transform.position);
    }
}
