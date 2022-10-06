using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    //[SerializeField] private PlayerTurn playerTurn;
    //[SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private Transform shootingStartPosition;
    //[SerializeField] private TrajectoryLine trajectoryLine;

    //private void Update()
    //{
    //    bool IsPlayerTurn = playerTurn.IsPlayerTurn();
    //    trajectoryLine.enabled = IsPlayerTurn;
    //    if (IsPlayerTurn)
    //    {
    //        Vector3 force = transform.forward * 700f + transform.up * 300f;
    //        trajectoryLine.DrawCurvedTrajectory(force, shootingStartPosition.position);
    //        if (Input.GetKeyDown(KeyCode.V))
    //        {
    //            TurnManager.GetInstance().TriggerChangeTurn();
    //            GameObject newProjectile = Instantiate(projectilePrefab);
    //            newProjectile.transform.position = shootingStartPosition.position;
    //            newProjectile.GetComponent<Projectile>().Initialize(force);
    //        }
    //    }
    //}
}
