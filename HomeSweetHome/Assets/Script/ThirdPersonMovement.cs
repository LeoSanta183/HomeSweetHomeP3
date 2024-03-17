using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float gravity = -9.81f;

    

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpearAttack(hitBoxes[0]);
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
        Vector3 gravityVector = Vector3.up * gravity;
        controller.Move(gravityVector * Time.deltaTime);

    }

    public Collider[] hitBoxes;

    private void SpearAttack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("HitBox"));
        foreach(Collider c in cols)
        {
            if(c.transform.root == transform)
            continue;
            

            float damage = 0;
            switch(c.name)
            {
            case "Body":
            damage = 10;
            Debug.Log(c.name);
            break;

            default:
            Debug.Log("Body not found");
            break;
            }

            col.GetComponent<Target>().TakeDamage(damage);
            
        }
    }


}
