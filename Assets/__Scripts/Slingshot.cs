using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    public GameObject projLinePrefab;

    [Header("Rubber Band Setup")]
    public LineRenderer lineRenderer; // Ref to linerender
    public Transform leftAnchor; // Left prong of sling
    public Transform rightAnchor; // Right arm of sling

    [Header("SFX")]
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip launchSFX;      // The sound effect to play when launching the ball


    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    
    void Start() {
        audioSource.volume = 0f; // Since mouseUp(0) will always be true when iterating through the first time
    }

    void OnMouseEnter() {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit() {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }


    void Awake() {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

        lineRenderer.positionCount = 2;
    }

    void OnMouseDown() {
        aimingMode = true;

        projectile = Instantiate(projectilePrefab) as GameObject;

        projectile.transform.position = launchPos;

        projectile.GetComponent<Projectile>().ResetSpeedBoost(); // Reset the speed boost for our new bullet

        projectile.GetComponent<Rigidbody>().isKinematic = true;

        UpdateRubberBand();
    }
    

    void Update() {
        if (!aimingMode) {
            return;
        }


        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D -launchPos;


        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        UpdateRubberBand();

        if (Input.GetMouseButtonUp(0) && aimingMode) {
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile = null;


            audioSource.PlayOneShot(launchSFX);
            ResetRubberBand();
            audioSource.volume = 1f;
        }
    }

    void UpdateRubberBand() {
        if (lineRenderer != null && projectile != null) {
            lineRenderer.SetPosition(0, leftAnchor.position);
            lineRenderer.SetPosition(1, projectile.transform.position);
        }
    }

    void ResetRubberBand() {
        if (lineRenderer != null) {
            lineRenderer.SetPosition(0, leftAnchor.position);
            lineRenderer.SetPosition(1, rightAnchor.position);
        }
    }

}
