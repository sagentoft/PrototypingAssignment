using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiringBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingStartPosition;
    //[SerializeField] private TrajectoryLine trajectoryLine;
    //public InputAction fireAction; Is an uneccesarry part of the "InputEditor. Since it's actually separate to InputSystem, despite being the reference in Manual.
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
            
        //}
    }
    private void OnFire()
    {
        Vector3 force = shootingStartPosition.forward;
        
        GameObject newProjectile = Instantiate(bulletPrefab);
        newProjectile.transform.position = shootingStartPosition.position;
        newProjectile.transform.localRotation = shootingStartPosition.rotation;
        newProjectile.GetComponent<Projectile>().Initialize(force);
       
        //RaycastHit result;
        //bool thereWasHit = Physics.Raycast(transform.position, transform.forward, out result, Mathf.Infinity);

        //// ------- The straight line trajectory code -------//
        //Vector3 start = transform.position;
        //Vector3 end = transform.position + transform.forward * 50f;
        ////lineRenderer.SetPosition(0, start);
        ////lineRenderer.SetPosition(1, end);

        //if (thereWasHit)
        //{
        //    result.collider.gameObject.GetComponent<MeshRenderer>().material.color = GetRandomColor();
        //}
    }

    //private Color GetRandomColor()
    //{
    //    Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    //    return color;
    //}
}
