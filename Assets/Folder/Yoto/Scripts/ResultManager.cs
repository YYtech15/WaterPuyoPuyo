using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject titleButton;
    [SerializeField] GameObject retryButton;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI judgeText;
    [SerializeField] AudioSource resultAudio;
    // int score = 800;
    public List<AudioClip> soundEffects;
    private AudioSource audioSource;
    string scoreStr;
    int score;
    public Image JudgeImg;
    public float fadeInTime = 2f;
    public Sprite[] judgeSprites;

    private void Start()
    {
        ScoreRecorder scoreRecorder;
        GameObject obj = GameObject.Find("ScoreRecorder");
        scoreRecorder = obj.GetComponent<ScoreRecorder>();
        audioSource = GetComponent<AudioSource>();
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
        GetComponent<AudioSource>().Play();
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
            PlaySound(0);
            judgeText.text = "A Cup!!";
            FadeAnim(0);
        }
        else if(score <= 600)
        {
            PlaySound(1);
            judgeText.text = "A Bucket!!";
            FadeAnim(1);
        }
        else if(score <= 1000)
        {
            PlaySound(2);
            judgeText.text = "A Bath!!";
            FadeAnim(2);
        }
        else if(score <= 1500)
        {
            PlaySound(3);
            judgeText.text = "Pool!!";
            FadeAnim(3);
        }
        else if(score <= 2000)
        {
            PlaySound(4);
            judgeText.text = "Lake!!";
            FadeAnim(4);
        }
        else
        {
            PlaySound(5);
            judgeText.text = "Sea!!";
            FadeAnim(5);
        }
    }

    public void ShowButtons()
    {
        titleButton.SetActive(true);
        retryButton.SetActive(true);
    }

    public void PlaySound(int soundIndex)
    {
        audioSource.clip = soundEffects[soundIndex];
        audioSource.Play();
    }

    public void FadeAnim(int animIndex)
    {
        JudgeImg.sprite = judgeSprites[animIndex];
        // JudgeImg.sprite.canvasRenderer.SetAlpha(0.0f);
        JudgeImg.CrossFadeAlpha(1f,fadeInTime,false);
    }
}
