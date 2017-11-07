using UnityEngine;
using System.Collections;

public class FishBowlInfo : MonoBehaviour
{
    private const float YaxisOffset = 7.5f;
    [SerializeField]
    private Vector3 size;

    [SerializeField]
    private GameObject forceTarget;

    private Vector3 RandomPosRange;
    public float forceTargetRadius;
    public bool enableForceTarget;

    private void Awake()
    {
        RandomPosRange = Vector3.Max(size - new Vector3(5, 5, 5), size * 0.8f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    public Vector3 GetRandomPosInBowl()
    {
        return new Vector3(Random.Range(-RandomPosRange.x / 2, RandomPosRange.x / 2),
            Random.Range(-RandomPosRange.y / 2, RandomPosRange.y / 2),
            Random.Range(-RandomPosRange.z / 2, RandomPosRange.z / 2));
    }

    public Vector3 GetRandomFishTarget(Vector3 original)
    {
        return (forceTarget == null || !enableForceTarget) ?
                new Vector3(Random.Range(-RandomPosRange.x / 2, RandomPosRange.x / 2),
                Mathf.Clamp(original.y + Random.Range(-YaxisOffset, YaxisOffset), -RandomPosRange.y / 2, RandomPosRange.y / 2),
                Random.Range(-RandomPosRange.z / 2, RandomPosRange.z / 2))
            : transform.InverseTransformPoint(forceTarget.transform.position);
    }

    public Vector3 GetForceTargetPos()
    {
        return transform.InverseTransformPoint(forceTarget.transform.position);
    }
}
