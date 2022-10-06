using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingScript : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;   
    }

    void Update()
    {
        Aim();
    }

    private (bool successful, Vector3 position) GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //The raycast hit something, returning the hitInfo as a position.
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            return (successful: true, position: hitInfo.point);
        else
            return (successful: false, position: Vector3.zero);
    }

    //Fetches the output of GetMousePosition to determine distance of the Raycast to the player????????
    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if(success == true)
        {
            //Calculate direction to be the position.
            Vector3 direction = position - transform.position;
            direction.y = 0;

            //Change the forward to be the new position.
            transform.forward = direction;
        }
    }
}
