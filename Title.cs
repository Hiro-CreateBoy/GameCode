using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;

    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start");

        if (!firstPush)
        {
            Debug.Log("Next");
            fade.StartFadeOut();
          /*  //ここに次のシーンに行く命令をかく
            SceneManager.LoadScene("stage1");
            */
            firstPush = true;
        }
    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete()) 
        {
            //ここに次のシーンに行く命令をかく
            SceneManager.LoadScene("stage1");
            goNextScene = true;
        }
    }
}
