using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool cursorLocked = true;

    public bool freeze;

    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpearAttack(hitBoxes[0]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            if (cursorLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }

        if (!cursorLocked)
        {
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public Collider[] hitBoxes;

    private void SpearAttack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("HitBox"));
        foreach(Collider c in cols)
        {
            if(c.transform.root == transform)
                continue;
            
            Debug.Log(c.name);

            float damage = 0;
            switch(c.name)
            {
                case "Body":
                damage = 1;
                break;

                default:
                Debug.Log("Body not found");
                break;
            }
        }
    }


}
