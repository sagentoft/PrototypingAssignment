using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody projectileBody;
    [SerializeField] private GameObject damageImdicatorPrefab;
    [SerializeField] private GameObject gunBarrel;

    private bool isActive;

    public void Initialize(Vector3 direction)
    {
        isActive = true;

        // -------- This method is for projectiles that have a parabole. ----------
        // We add a force only once, not every frame, as it is not needed in this case.
        // Make sure to have "useGravity" toggled on in the rigid body, this is bcz of the different option in Gravity.
        //projectileBody.AddForce(direction);
    }

    void Update()
    {
        if (isActive)
        {
            // -------- This method is for projectiles that go in a straight line. ----------
            // We change the position every frame in order for it to
            // Make sure to have "useGravity" toggled off in the rigid body, otherwise it will fall as it flies (unless thats what you want)

            // Use either the following line (movement with the rigid body)
            projectileBody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            

            // or this one (movement with the transform), both are ok
            //transform.Translate(transform.forward * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().TakeDamage(10);
            Destroy(gameObject);
        }
     
        Destroy(gameObject);
        
        //GameObject damageIndicator = Instantiate(damageImdicatorPrefab);
        //damageIndicator.transform.position = collision.GetContact(0).point;
    }
}
