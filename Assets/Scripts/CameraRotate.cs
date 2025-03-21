using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public Transform targetTransform;
    public Vector3 TargetPosition
    {
        get { return target; }
        set { target = value; }
    }
    private Vector3 target;
    public float amp = 2.0f;

    void Awake()
    {
        if (!targetTransform)
        {
            target = TargetPosition;
        }
        else
            target = targetTransform.position;
        transform.LookAt(target);
    }

    void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.right * amp * Time.deltaTime);
    }
}
