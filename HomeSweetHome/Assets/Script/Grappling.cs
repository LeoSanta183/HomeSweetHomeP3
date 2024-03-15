using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    private ThirdPersonMovement pm;
    public Transform player;
    public Transform gunTip;
    public LayerMask whatIsGrappable;
    public LineRenderer lr;

    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    public float grapplingCd;
    private float grapplingCdTimer;

    public KeyCode grappleKey = KeyCode.Space;

    public bool grappling;


    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (grappling)
            lr.SetPosition(0, gunTip.position);
    }
    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(player.position, player.forward, out hit, maxGrappleDistance, whatIsGrappable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = player.position + player.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        Vector3 grappleDirection = (grapplePoint - player.position).normalized;
        float distanceToGrapplePoint = Vector3.Distance(player.position, grapplePoint);

        
        pm.controller.Move(grappleDirection * distanceToGrapplePoint);

        
        StopGrapple();
    }

    private void StopGrapple()
    {
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;
    }

}
