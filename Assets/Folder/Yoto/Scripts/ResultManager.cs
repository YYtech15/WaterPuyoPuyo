using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject titleButton;
    [SerializeField] GameObject retryButton;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI judgeText;
    // int score = 800;
    string scoreStr;
    int score;

    private void Start()
    {
        ScoreRecorder scoreRecorder;
        GameObject obj = GameObject.Find("ScoreRecorder");
        scoreRecorder = obj.GetComponent<ScoreRecorder>();
        score = scoreRecorder.score;
        titleButton.SetActive(false);
        retryButton.SetActive(false);
        Debug.Log(score);
        StartCoroutine(ShowResult());
    }

    // スコアを表示する関数
    IEnumerator ShowResult()
    {
        // 記録オブジェクトの持つスコアを文字に変換する
        scoreStr = score.ToString();
        // scoreStr = score.ToString(); 
        yield return new WaitForSeconds(1f);
        // 変換した文字をテキストに入れて表示する
        scoreText.text = scoreStr + "L";
        yield return new WaitForSeconds(1f);
        JudgeAmount();
        yield return new WaitForSeconds(1f);
        ShowButtons();
    }

    public void OnRetryButton()
    {
        // SceneManager.LoadScene("Game");
    }

    public void OnTitleScene()
    {
        // SceneManager.LoadScene("Title");
    }

    private void JudgeAmount()
    {
        if(score <= 300)
        {
            judgeText.text = "あなたの得た水量はおちょこ程度";
        }
        else if(score <= 600)
        {
            judgeText.text = "あなたの得た水量はコップ1杯程度";
        }
        else if(score <= 1000)
        {
            judgeText.text = "あなたの得た水量はバケツ１杯程度";
        }
        else if(score <= 1500)
        {
            judgeText.text = "あなたの得た水量はプール程度";
        }
        else if(score <= 2000)
        {
            judgeText.text = "あなたの得た水量は湖程度";
        }
        else
        {
            judgeText.text = "あなたの得た水量は海程度";
        }
    }

    public void ShowButtons()
    {
        titleButton.SetActive(true);
        retryButton.SetActive(true);
    }
}
