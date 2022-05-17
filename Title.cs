using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [Header("�t�F�[�h")] public FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;

    //�X�^�[�g�{�^���������ꂽ��Ă΂��
    public void PressStart()
    {
        Debug.Log("Press Start");

        if (!firstPush)
        {
            Debug.Log("Next");
            fade.StartFadeOut();
          /*  //�����Ɏ��̃V�[���ɍs�����߂�����
            SceneManager.LoadScene("stage1");
            */
            firstPush = true;
        }
    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete()) 
        {
            //�����Ɏ��̃V�[���ɍs�����߂�����
            SceneManager.LoadScene("stage1");
            goNextScene = true;
        }
    }
}
