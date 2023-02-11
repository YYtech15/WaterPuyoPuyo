using UnityEngine;

public class BGMLoopController : MonoBehaviour
{
    public AudioSource AudioSource;
    public float LoopEndTime;       //ループの終端時間(秒)
    public float LoopLengthTime;    //ループの長さ(秒)
    
    private void Update()
    {
        if(AudioSource.time >= LoopEndTime)
        {
            AudioSource.time -= LoopLengthTime;
        }
    }
}