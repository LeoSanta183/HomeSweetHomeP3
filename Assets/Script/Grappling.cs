using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    private PlayerMovement pm;
    public Transform player;
    public Transform gunTip;
    public LayerMask whatIsGrappable;
    public LineRenderer lr;

    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    public float grapplingCd;
    private float grapplingCdTimer;

    public KeyCode grappleKey = KeyCode.Space;

    public bool grappling;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey) || Input.GetAxis("LeftTrigger") > 0) 
            StartGrapple();
            


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
        pm.grappleAnimate();
        grappling = true;

        pm.freeze = true;

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
        audioManager.PlaySFX(audioManager.Grapple);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;
        pm.grappleFinished();
    }

}
