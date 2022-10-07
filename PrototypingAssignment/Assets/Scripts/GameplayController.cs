using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI countDown;

    private float timeLeft = 300f;

    private int killCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        countDown.text = "Time left: " + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("DashLevel");
        }
    }

    public void EnemyKilled()
    {
        killCount++;
        killCountText.text = "Enemies killed: " + killCount;
    }

    
}
