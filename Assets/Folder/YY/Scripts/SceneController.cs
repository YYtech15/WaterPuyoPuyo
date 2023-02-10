using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //遷移先のシーン
    [SerializeField]private string nextScene;

    public void SwitchScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}