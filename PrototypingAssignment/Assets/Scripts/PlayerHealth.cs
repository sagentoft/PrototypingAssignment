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

    public void Start()
    {
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
        //Debug.Log(amount);
        Debug.Log(health);
        if (health <= 0)
        {            
            SceneManager.LoadScene("DashLevel");         
        }
    }

}
