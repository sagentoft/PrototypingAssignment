using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HP;
    [Range(0, 40)] public int health;
    public int maxHealth = 40;
    private Transform playerBody;
    private MeshRenderer playerRenderer;
    private Color startColor;

    public void Start()
    {
        playerBody = transform.GetChild(0);
        playerRenderer = playerBody.GetComponent<MeshRenderer>();
        startColor = playerRenderer.material.color;
        health = maxHealth;
        //Debug.Log(health);
    }

    private void Update()
    {
        HP.text = "Hp: ";
       HP.text += health.ToString();
       HP.color = Color.yellow;
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        StartCoroutine(DamageFlash());

        if (health <= 0)
        {            
            SceneManager.LoadScene("DashLevel");         
        }
    }

    private IEnumerator DamageFlash()
    {
        playerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerRenderer.material.color = startColor;
    }

}
