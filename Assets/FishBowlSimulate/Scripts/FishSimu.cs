using UnityEngine;
using System.Collections;

public class FishSimu : MonoBehaviour
{
    private const float MaxVelority = 0.55f;
    private const float MinVelority = 0.25f;
    private const float VelorityReducePerSecond = 0.4f;
    private const float StepMaxInterval = 7f;

    public FishBowlInfo bowl;
    [SerializeField]
    private Animator anim;

    private Vector3 targetPos;
    private bool needNewTargetPos = true;

    private float curStepRemain;
    private float velority;
    private float curStepMinVelority;

    private float minDistance;

    private bool forceTarget;

    private bool rotating;

    private void Start()
    {
        transform.position = bowl.transform.TransformPoint(bowl.GetRandomPosInBowl());
    }

    private void Update()
    {
        if (bowl != null)
        {
            FishMove();
        }
    }

    private void GetRandomPosInBowl(bool limitY = true)
    {
        targetPos = limitY ? bowl.GetRandomFishTarget(bowl.transform.InverseTransformPoint(transform.position)) : bowl.GetRandomPosInBowl();
        //transform.LookAt(bowl.transform.TransformPoint(targetPos));
        minDistance = Vector3.Distance(transform.position, bowl.transform.TransformPoint(targetPos));
        needNewTargetPos = false;
        forceTarget = bowl.enableForceTarget;
    }

    private void GetRandomVelority()
    {
        velority = Random.Range(MinVelority, MaxVelority);
        curStepMinVelority = Random.Range(0, MinVelority);
        curStepRemain = Random.Range(0, StepMaxInterval);
    }

    private void FishMove()
    {
        if (forceTarget)
        {
            FishForceTargetPosUpdate();
        }
        else
        {
            FishPosUpdate();
        }
        FishPropUpdate();
        FishAngleUpdate();
    }

    private void FishAngleUpdate()
    {
        rotating = Vector3.Angle(transform.forward, bowl.transform.TransformPoint(targetPos) - transform.position) > 3;

        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, bowl.transform.TransformPoint(targetPos) - transform.position, Color.yellow);

        var transInBowl = bowl.transform.InverseTransformPoint(transform.position);
        var targetNormalize = transInBowl + (targetPos - transInBowl).normalized;

        Debug.DrawLine(transform.position, bowl.transform.TransformPoint(targetNormalize), Color.black);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.white);

        var t = (transform.position+transform.forward + bowl.transform.TransformPoint(targetNormalize)) / 2;

        Debug.DrawLine(transform.position, t, Color.green);

        transform.LookAt(t);
    }

    private void FishPropUpdate()
    {
        if (velority <= curStepMinVelority)
        {
            if (curStepRemain <= 0)
            {
                GetRandomVelority();
            }
        }
        if (velority > curStepMinVelority)
        {
            velority -= Time.deltaTime * VelorityReducePerSecond;
        }
        if (velority < curStepMinVelority)
        {
            velority = curStepMinVelority;
        }
        anim.SetFloat("Speed", velority * 4 + 0.7f);
        curStepRemain -= Time.deltaTime;
    }

    private void FishPosUpdate()
    {
        if (needNewTargetPos)
        {
            GetRandomPosInBowl();
        }
        transform.position = transform.position + transform.forward * (rotating ? MinVelority / 2 : velority);
        var distance = Vector3.Distance(transform.position, bowl.transform.TransformPoint(targetPos));
        if (distance < 2 * MinVelority)
        {
            needNewTargetPos = true;
        }
        else
        {
            minDistance = distance;
        }
    }

    private void FishForceTargetPosUpdate()
    {
        if (!bowl.enableForceTarget && forceTarget)
        {
            forceTarget = false;
            GetRandomPosInBowl(false);
            return;
        }

        targetPos = bowl.transform.TransformPoint(bowl.GetForceTargetPos());
        var distance = Vector3.Distance(transform.position, bowl.transform.TransformPoint(bowl.GetForceTargetPos()));

        //transform.LookAt(bowl.transform.TransformPoint(bowl.GetForceTargetPos()));

        if (distance <= bowl.forceTargetRadius)
        {
        }
        else
        {
            transform.position = transform.position + transform.forward * velority;
        }
    }
}
