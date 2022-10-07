using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiringBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingStartPosition;
    [SerializeField] public int _ammoCount = 5;
    [SerializeField] AudioClip emptyClipSound;
    [SerializeField] AudioClip firingSound;
    //public InputAction fireAction; Is an uneccesarry part of the "InputEditor. Since it's actually separate to InputSystem, despite being the reference in Manual.

    private IEnumerator Reload()
    {
        Debug.Log("Started Reload Coroutine" + Time.time);

        yield return new WaitForSeconds(1);

        Debug.Log("Ending Reload Coroutine" + Time.time);
        _ammoCount = 5;
    }
    private void OnFire()
    {
        
        Vector3 force = shootingStartPosition.forward;

        if (_ammoCount > 0)
        {
            GameObject newProjectile = Instantiate(bulletPrefab);
            newProjectile.transform.position = shootingStartPosition.position;
            newProjectile.transform.localRotation = shootingStartPosition.rotation;
            newProjectile.GetComponent<Projectile>().Initialize(force);
            AudioSource.PlayClipAtPoint(firingSound, transform.position);
            AmmoCount(-1);
        }
        else if (_ammoCount <= 0)
        {
            AudioSource.PlayClipAtPoint(emptyClipSound, transform.position);
            StartCoroutine(Reload());
        }
        else
            return;

    }

    public void AmmoCount(int amountBullets)
    {
        _ammoCount += amountBullets;
    }
}
