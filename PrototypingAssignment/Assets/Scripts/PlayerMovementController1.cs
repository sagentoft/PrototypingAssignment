using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController1 : MonoBehaviour
{
    [Header("Base Speed Values")]
    public float maximumSpeed;
    public float gravityValue;

    [Header("Acceleration Curves")]
    public AnimationCurve accelerationCurve;
    public AnimationCurve decelerationCurve;
    public float accelerationDuration;
    public float brakeDuration;

    //The speed of the character is name targetSpeed instead, very odd. Is it bcz movement calculation doesnt work linearly?
    private float targetSpeed;
    private float speedChangeTimer = 0;
    private float lastVelocitybeforeDeceleration = 0;

    //A variable of the Component.
    private CharacterController controller;
    //Reference to the player position.
    private Transform playerBody;
    //private new Ga
    private Vector3 movementVector;
    private Vector3 lastMaxMovementVector;

    private Vector2 leftStickPosition;
    private Vector2 rightStickPosition;

    [HideInInspector]
    public bool isInputEnabled;
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
            //Checks if the player has stopped moving the stick - Deceleration
            if(speedChangeTimer > 0)
            {
                //This happens only once per deceleration at the first frame
                if (!isDecelerating)
                {
                    isDecelerating = true;
                    lastVelocitybeforeDeceleration = movementVector.magnitude;
                }

                if (speedChangeTimer > brakeDuration)
                    speedChangeTimer = brakeDuration; //Clamping the time value to the deceleration

                float ratio = lastVelocitybeforeDeceleration < maximumSpeed ? lastVelocitybeforeDeceleration / maximumSpeed : 1; //Ternary operator, useful to avoid small if/else statements

                speedChangeTimer -= dt;
                progression = Mathf.Clamp(speedChangeTimer / brakeDuration, 0, 1);
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

        if(rightStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStickPosition.x, rightStickPosition.y) * Mathf.Rad2Deg;
            playerBody.rotation = Quaternion.Euler(0, angle - 90, 0);
        }
    }
}

