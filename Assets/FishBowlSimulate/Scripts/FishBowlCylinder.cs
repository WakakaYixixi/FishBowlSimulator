using UnityEngine;
using System.Collections;

public class FishBowlCylinder : FishBowlInfo
{
    [SerializeField]
    private Vector3 Center;
    [SerializeField]
    private float Radius;
    [SerializeField]
    private float Height;

    private float RandomPosRadius;
    private float RandomPosHeight;

    protected override void CalculateRange()
    {
        RandomPosRadius = Mathf.Max(Radius - 5, Radius * 0.8f);
        RandomPosHeight = Mathf.Max(Height - 5, Height * 0.8f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Center + new Vector3(Radius, -Height / 2, 0), transform.position + Center + new Vector3(Radius, Height / 2, 0));
        Gizmos.DrawLine(transform.position + Center + new Vector3(-Radius, -Height / 2, 0), transform.position + Center + new Vector3(-Radius, Height / 2, 0));
        Gizmos.DrawLine(transform.position + Center + new Vector3(0, -Height / 2, Radius), transform.position + Center + new Vector3(0, Height / 2, Radius));
        Gizmos.DrawLine(transform.position + Center + new Vector3(0, -Height / 2, -Radius), transform.position + Center + new Vector3(0, Height / 2, -Radius));
        var temp = Radius * Mathf.Sqrt(2) / 2;
        Gizmos.DrawLine(transform.position + Center + new Vector3(temp, -Height / 2, temp), transform.position + Center + new Vector3(temp, Height / 2, temp));
        Gizmos.DrawLine(transform.position + Center + new Vector3(-temp , -Height / 2, -temp), transform.position + Center + new Vector3(-temp, Height / 2, -temp));
        Gizmos.DrawLine(transform.position + Center + new Vector3(-temp, -Height / 2, temp), transform.position + Center + new Vector3(-temp, Height / 2, temp));
        Gizmos.DrawLine(transform.position + Center + new Vector3(temp, -Height / 2, -temp), transform.position + Center + new Vector3(temp, Height / 2, -temp));


        UnityEditor.Handles.DrawWireDisc(transform.position + Center + new Vector3(0, Height / 2, 0), transform.up, Radius);
        UnityEditor.Handles.DrawWireDisc(transform.position + Center + new Vector3(0, -Height / 2, 0), transform.up, Radius);
    }

    public override Vector3 GetRandomPosInBowl()
    {
        var xRandom = Random.Range(-RandomPosRadius, RandomPosRadius);
        var limit = Mathf.Abs(RandomPosRadius * Mathf.Cos(Mathf.Asin(Mathf.Abs(xRandom) / RandomPosRadius)));
        var zRandom = Random.Range(-limit, limit);
        var yRandom = Random.Range(-RandomPosHeight / 2, RandomPosHeight / 2);
        Debug.Log(new Vector3(xRandom, yRandom, zRandom));
        return new Vector3(xRandom, yRandom, zRandom);
    }

    public override Vector3 GetRandomFishTarget(Vector3 original)
    {
        if ((forceTarget == null || !enableForceTarget))
        {
            var xRandom = Random.Range(-RandomPosRadius, RandomPosRadius);
            var limit = Mathf.Abs(RandomPosRadius * Mathf.Cos(Mathf.Asin(Mathf.Abs(xRandom) / RandomPosRadius)));
            var zRandom = Random.Range(-limit, limit);
            var yRandom = Mathf.Clamp(original.y + Random.Range(-YaxisOffset, YaxisOffset), -RandomPosHeight / 2, RandomPosHeight / 2);
            return new Vector3(xRandom, yRandom, zRandom);
        }
        else
        {
            return transform.InverseTransformPoint(forceTarget.transform.position);
        }
    }
}
