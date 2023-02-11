using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    static public BGMController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //既にオブジェクトがあったら削除
            Destroy(this.gameObject);
        }
    }

    public AudioSource CurrentBGM;  //現在のBGM
    public AudioSource MovedBGM;    //移動先のBGM

    private string CurrentSceneName;
    public string MovedSceneName;

    void Start()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
        CurrentBGM.Play();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わった時に呼ばれるメソッド　
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
            //シーンがどう変わったかで判定
            if (nextScene.name == MovedSceneName)
            {
              CurrentBGM.Stop();
              MovedBGM.Play();
            }
             if (nextScene.name == CurrentSceneName)
             {
              CurrentBGM.Play();
              MovedBGM.Stop();
             }
    }
}