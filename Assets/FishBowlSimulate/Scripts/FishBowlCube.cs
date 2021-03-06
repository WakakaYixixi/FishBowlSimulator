﻿using UnityEngine;
using System.Collections;

public class FishBowlCube : FishBowlInfo
{
    [SerializeField]
    private Vector3 size;

    private Vector3 RandomPosRange;

    protected override void CalculateRange()
    {
        RandomPosRange = new Vector3(Mathf.Max(size.x - 5, size.x * 0.8f), Mathf.Max(size.y - 5, size.y * 0.8f), Mathf.Max(size.z - 5, size.z * 0.8f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    public override Vector3 GetRandomPosInBowl()
    {
        return new Vector3(Random.Range(-RandomPosRange.x / 2, RandomPosRange.x / 2),
            Random.Range(-RandomPosRange.y / 2, RandomPosRange.y / 2),
            Random.Range(-RandomPosRange.z / 2, RandomPosRange.z / 2));
    }

    public override Vector3 GetRandomFishTarget(Vector3 original)
    {
        return (forceTarget == null || !enableForceTarget) ?
                new Vector3(Random.Range(-RandomPosRange.x / 2, RandomPosRange.x / 2),
                Mathf.Clamp(original.y + Random.Range(-YaxisOffset, YaxisOffset), -RandomPosRange.y / 2, RandomPosRange.y / 2),
                Random.Range(-RandomPosRange.z / 2, RandomPosRange.z / 2))
            : transform.InverseTransformPoint(forceTarget.transform.position);
    }
}
