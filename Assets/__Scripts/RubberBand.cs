using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform LeftArm;
    public Transform RightArm;
    public Transform projectile;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, LeftArm.position);
        lineRenderer.SetPosition(1, RightArm.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile != null) {
            lineRenderer.SetPosition(0, LeftArm.position);
            lineRenderer.SetPosition(1, projectile.position);
        }
    }
}
