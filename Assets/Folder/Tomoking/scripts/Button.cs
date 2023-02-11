using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string NextScene = "TestScene"; // 移動先のシーン名

    // ボタンをクリックしたら次のシーンへ移動
    public void OnClickButton() {
        SceneManager.LoadScene(NextScene);
    }
}
