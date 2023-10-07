using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public PlayerCombat playerCombat;
    public Transform groundTracker;
    public LayerMask groundMask;
    public PlayerManager manager;

    public float speed = 10f;
    public float rotationSmoothing = 0.1f;
    public float rotationSmoothingVel;

    public float gravity = -9.8f;

    public float rollSpeed = 20f;
    public float rollTime = 0.5f;

    public float rollCD = 1f;

    


    float yVel;

    float xMovement, zMovement;
    Vector3 directionVector, moveDirection;

    bool isGrounded;

    float rollTimer;

    
    // Update is called once per frame
    void Update()
    {
        UpdateGravity();

        rollTimer -= Time.deltaTime;
        if (!manager.getBusy())
        {
            float forwardAngle;
            xMovement = Input.GetAxisRaw("Horizontal");
            zMovement = Input.GetAxisRaw("Vertical");

            directionVector = new Vector3(xMovement, 0f, zMovement).normalized;
            if(playerCombat.GetFocused() != null)
            {
                transform.LookAt(playerCombat.GetFocused().transform.position);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                forwardAngle = transform.rotation.eulerAngles.y;
            }
            else
            {
                forwardAngle = cam.eulerAngles.y;
            }
            if (directionVector.sqrMagnitude > 0)
            {


                float rotationAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg + forwardAngle;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref rotationSmoothingVel, rotationSmoothing);
                if (playerCombat.GetFocused() != null)
                {
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                    controller.Move(moveDirection  * speed * Time.deltaTime);
                }
                else 
                { 
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    controller.Move(transform.rotation * Vector3.forward * speed * Time.deltaTime);
                }


                
                



            }

            animator.SetFloat("velX", directionVector.x, 0.1f, Time.deltaTime);
            animator.SetFloat("velZ", directionVector.z, 0.1f, Time.deltaTime);
            animator.SetFloat("vel", directionVector.magnitude, 0.1f, Time.deltaTime);

            
            if (Input.GetKeyDown(KeyCode.Space) && rollTimer <= 0)
            {
                StartCoroutine(Roll());
            }
        }
    }

    void UpdateGravity()
    {
        isGrounded = Physics.CheckSphere(groundTracker.position, 0.2f, groundMask);

        if (!isGrounded)
        {
            yVel += gravity * Time.deltaTime;
            controller.Move(yVel * Time.deltaTime * Vector3.up);
        }
        else
        {
            yVel = 0f;
        }
    }

    

    IEnumerator Roll()
    {
        float startTime = Time.time;
        rollTimer = rollCD;
        animator.SetBool("Roll", true);
        while(Time.time < startTime + rollTime)
        {
            controller.Move(moveDirection * Time.deltaTime * rollSpeed);

            yield return null;
        }
        animator.SetBool("Roll", false);
    }   

    public IEnumerator Step(Vector3 dir, float time, float speed)
    {
        float startTime = Time.time;
        while (Time.time < startTime + time)
        {
            controller.Move(dir * Time.deltaTime * speed );

            yield return null;
        }
    }
}
