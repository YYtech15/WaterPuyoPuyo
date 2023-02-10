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
        // スコア表示をするテキストオブジェクトを取得
        currentScoreText = GetComponent<TextMeshProUGUI>();
        // スコア変数
        score = 0;
        // スコアレコーダーの保持
        DontDestroyOnLoad(this.gameObject);
    }

    // 水が消えるたびにポイントを加算する
    void Update()
    {
        // if()
        // {
        //     AddScore();
        // }
    }

    // スコアを加算する関数
    public void AddScore()
    {
        score += AddPoint;
        currentScoreText.text = score.ToString() + "L"; 
    }
}
