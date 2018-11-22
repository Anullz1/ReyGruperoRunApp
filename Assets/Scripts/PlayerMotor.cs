using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    private Rigidbody thisrigid;
    private const float LANE_DISTANCE = 4.5f;
    private const float TURN_SPEED = 0.02f;

    //Funcionality
    private bool isRunning = false;


    //Animation
    private Animator anim;



    //movement

    private CharacterController controller;
    private float jumpForce = 12.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private float speed = 10.0f;
    private int desiredLane = 1; //0=left, 1= middle, 2=derecha

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
     
    }
    private void Update()
    {
        if (!isRunning)
            return;

        ////gather the inputs on wich lane we should be
       // if (Input.GetKeyDown(KeyCode.LeftArrow))
         //   MoveLane(false);//moveleft
        //if (Input.GetKeyDown(KeyCode.RightArrow))
           // MoveLane(true);//move right

        if (MobileInput.Instance.SwipeLeft)
            MoveLane(false);
        if (MobileInput.Instance.SwipeRight)
            MoveLane(true);

        //calculate we're should be in the future

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        // Lets calculate our move delta

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;


        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        //calcuate Y this is for Jumps

        if (IsGrounded())
        // if grounded
        {
            verticalVelocity = -0.1f;
           

            //if(Input.GetKeyDown(KeyCode.Space))
            if(MobileInput.Instance.SwipeUp)
            {

                Jumpy();
                Invoke("StopJumpy", 1.15f);
                verticalVelocity = jumpForce;
            }
            //else if (Input.GetKeyDown(KeyCode.DownArrow))
            else if(MobileInput.Instance.SwipeDown)
            {
                //slide
                StartSliding();
                Invoke("StopSliding", 1.0f);
                
            }
        
        }
        else 
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            //fast falling
            if(MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }

        }


        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //move Caharactter
        controller.Move(moveVector * Time.deltaTime);

        //Rotate the character forwad position

        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
       
    }
    private void Jumpy()
    {
        anim.SetBool("Jumpy", true);
     
    }
    private void StopJumpy()
    {
        anim.SetBool("Jumpy", false);

    }
    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }
    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }
    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);


        //if (!goingRight)
        //{
        //    desiredLane--;
        //    if (desiredLane == -1)
        //        desiredLane = 0;
        //}
        //else
        //{
        //desiredLane++;
        //if (desiredLane == 3)
        //desiredLane = 2;
    }
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x,(controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,controller.bounds.center.z), Vector3.down); Debug.DrawRay(groundRay.origin, groundRay.direction, Color.red, 1.0f);
        return Physics.Raycast(groundRay, 0.2f + 0.2f);
       
    }
    public void StartRunning()
    {

        isRunning = true;
        anim.SetTrigger("StartRunning");
    }
    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch(hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }

}
