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

    public AudioSource MovedBGM;    //移動先のBGM

    public string MovedSceneName;

    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わった時に呼ばれるメソッド　
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
            if (nextScene.name == MovedSceneName)
            {
                MovedBGM.Play();
            }
            else {
                MovedBGM.Stop();
            }
    }
}