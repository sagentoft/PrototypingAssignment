using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Base Speed Values")]
    public float maximumSpeed;
    public float gravityValue;

    [Header("Acceleration Curves")]
    public AnimationCurve accelerationCurve;
    public AnimationCurve decelerationCurve;
    public float accelerationDuration;
    public float decelerationDuration;

    private float targetSpeed;
    private float speedChangeTimer = 0;
    private float lastVelocitybeforeDeceleration = 0;

    private CharacterController controller;
    private Transform playerBody;

    private Vector3 movementVector;
    private Vector3 lastMaxMovementVector;

    private Vector2 leftStickPosition;
    private Vector2 rightStickPosition;

    [HideInInspector]
    public bool isInputEnabled;
    public bool controllerConnected;
    private bool isDecelerating = false;
    


    //@INIT
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerBody = transform.GetChild(0);
        movementVector = Vector3.zero;
        targetSpeed = maximumSpeed;
    }

    void Update()
    {
        //Checks if a controller is connected.
        if (Input.GetJoystickNames().Length >= 0)
        {
            controllerConnected = false;
            //LookAtMouse();
        }


        float dt = Time.deltaTime; //It's used quite a lot in this function so I am storing it locally for a light optimization

        controller.Move(new Vector3(0, -gravityValue * dt, 0)); //gravity: the character constantly moves downwards, the CharacterController component handles collision
        
        float progression = 0;

        //What happens when the stick is pressed in a direction
        if(leftStickPosition != Vector2.zero)
        {
            isDecelerating = false;

            //Acceleration
            if(speedChangeTimer <= accelerationDuration)
            {
                speedChangeTimer += dt;
                progression = Mathf.Clamp(speedChangeTimer / accelerationDuration, 0, 1);
                targetSpeed = maximumSpeed * accelerationCurve.Evaluate(progression);

                movementVector = Vector3.Lerp(Vector3.zero, new Vector3(leftStickPosition.x, 0, leftStickPosition.y) * targetSpeed, progression);
                lastMaxMovementVector = movementVector.normalized * maximumSpeed;
            }
            //Top speed
            else
            {
                movementVector = new Vector3(leftStickPosition.x, 0, leftStickPosition.y) * maximumSpeed;
                lastMaxMovementVector = movementVector;
            }
        }
        else
        {
            //Deceleration
            if(speedChangeTimer > 0)
            {
                //This happens only once per deceleration at the first frame
                if (!isDecelerating)
                {
                    isDecelerating = true;
                    lastVelocitybeforeDeceleration = movementVector.magnitude;
                }

                if (speedChangeTimer > decelerationDuration)
                    speedChangeTimer = decelerationDuration; //Clamping the time value to the deceleration

                float ratio = lastVelocitybeforeDeceleration < maximumSpeed ? lastVelocitybeforeDeceleration / maximumSpeed : 1; //Ternary operator, useful to avoid small if/else statements

                speedChangeTimer -= dt;
                progression = Mathf.Clamp(speedChangeTimer / decelerationDuration, 0, 1);
                movementVector = Vector3.Lerp(Vector3.zero, lastMaxMovementVector * ratio, progression);
            }
        }

        //All the previous code only serves to calculate the movementVector, and we apply the movement to the controller at the very end of the Update
        controller.Move(movementVector * dt);
    }

    //@INPUT: Gather the stick coordinates
    private void OnMove(InputValue movementValue)
    {
        leftStickPosition = movementValue.Get<Vector2>();
    }

    //@INPUT: Handle transform rotation
    private void OnLook(InputValue lookValue)
    {

        rightStickPosition = lookValue.Get<Vector2>();

        if (rightStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStickPosition.x, rightStickPosition.y) * Mathf.Rad2Deg;
            playerBody.rotation = Quaternion.Euler(0, angle - 90, 0);
        }
    }
    private void LookAtMouse()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.forward = mousePos - new Vector2(transform.position.x, transform.position.y);
    }
}
