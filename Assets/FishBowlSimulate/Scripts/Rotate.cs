using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private GameObject rotateArround;
    [SerializeField]
    private float speed = 1;

    private void Update()
    {
        transform.RotateAround(rotateArround.transform.position, Vector3.up, speed);
    }
}
