using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRecorder : MonoBehaviour
{
    public int score;
    private int AddPoint = 100;
    [SerializeField] TextMeshProUGUI currentScoreText;

    void Start()
    {
        // スコア変数
        score = 0;
        currentScoreText.text = score.ToString();
        // スコアレコーダーの保持
        DontDestroyOnLoad(this.gameObject);
    }

    // スコアを加算する関数
    public void AddScore()
    {
        score += AddPoint;
        currentScoreText.text = score.ToString() + "L"; 
    }
}
