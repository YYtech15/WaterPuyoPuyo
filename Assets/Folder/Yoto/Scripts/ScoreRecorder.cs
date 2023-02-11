using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRecorder : MonoBehaviour
{
    public int score;
    public int AddPoint = 100;
    [SerializeField] TextMeshProUGUI currentScoreText;
    GameManager gameManager;

    void Start()
    {
        GameObject obj = GameObject.Find("GameManager");
        gameManager = obj.GetComponent<GameManager>();
        // スコア変数
        score = 0;
        currentScoreText.text = score.ToString();
        // スコアレコーダーの保持
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        currentScoreText.text = score.ToString();
    }

    // スコアを加算する関数
    public void AddScore()
    {
        score += AddPoint * gameManager.rensa;
        currentScoreText.text = score.ToString() + "mL"; 
    }
}
