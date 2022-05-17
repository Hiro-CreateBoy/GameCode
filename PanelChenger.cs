using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelChenger : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;


    #region//矢印をクリックしたら、場面移動
    public void OnAllow0()
    {
        //押したときに説明パネルに行く
        this.transform.localPosition = new Vector2(-1000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow1()
    {
        //押したときにゲームを始める
        this.transform.localPosition = new Vector2(-2000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow2()
    {
        //押したときに第二ステージへ
        this.transform.localPosition = new Vector2(-2000, 1000);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow3()
    {
        this.transform.localPosition = new Vector2(0, 1000);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow4()
    {
        this.transform.localPosition = new Vector2(-3000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow5()
    {
        this.transform.localPosition = new Vector2(-4000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow6()
    {
        this.transform.localPosition = new Vector2(-5000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow7()
    {
        this.transform.localPosition = new Vector2(-6000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow4kai()
        {
        this.transform.localPosition = new Vector2(-2000, 1000);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow8()
    {
        this.transform.localPosition = new Vector2(-6000, 1000);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow9()
    {
        this.transform.localPosition = new Vector2(-7000, 0);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow10()
    {
        this.transform.localPosition = new Vector2(-7000, 1000);
        audioSource.PlayOneShot(sound1);
    }
    public void OnAllow11()
    {
        SceneManager.LoadScene("GameScene");
        audioSource.PlayOneShot(sound1);
    }
    #endregion

    #region//1ステージのアイテムを定義する
    public GameObject BoxItem0;
    public GameObject BoxItem1;
    public GameObject BoxItem2;
    public GameObject BoxItem3;
    private bool Doku;
    #endregion

    #region//stge2
    public GameObject BoxItem4;
    public GameObject BoxItem5;
    public GameObject BoxItem6;
    public GameObject BoxItem7;
    private bool Kiss;
    private bool Nuno;
    #endregion

    #region//stge3
    public GameObject BoxItem8;
    public GameObject BoxItem9;
    public GameObject BoxItem10;
    public GameObject BoxItem11;
    private bool Unchi;
    #endregion

    #region//stage4
    public GameObject BoxItem12;
    public GameObject BoxItem13;
    public GameObject BoxItem14;
    public GameObject BoxItem15;
    private bool Te;
    #endregion

    #region//stage5
    public GameObject BoxItem16;
    public GameObject BoxItem17;
    public GameObject BoxItem18;
    public GameObject BoxItem19;
    private bool Heart;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region//ステージ１のボックスに入るアイテムを消しておく
        BoxItem0.SetActive(false);
        BoxItem1.SetActive(false);
        BoxItem2.SetActive(false);
        BoxItem3.SetActive(false);
        Doku = false;
        #endregion
        #region//stge2
        BoxItem4.SetActive(false);
        BoxItem5.SetActive(false);
        BoxItem6.SetActive(false);
        BoxItem7.SetActive(false);
        Kiss = false;
        Nuno = false;
        #endregion
        #region//srge3
        BoxItem8.SetActive(false);
        BoxItem9.SetActive(false);
        BoxItem10.SetActive(false);
        BoxItem11.SetActive(false);
        Unchi = false;
        #endregion
        #region//stage4
        BoxItem12.SetActive(false);
        BoxItem13.SetActive(false);
        BoxItem14.SetActive(false);
        BoxItem15.SetActive(false);
        Te = false;
        #endregion
        #region//stage5
        BoxItem16.SetActive(false);
        BoxItem17.SetActive(false);
        BoxItem18.SetActive(false);
        BoxItem19.SetActive(false);
        Heart = false;
        #endregion

        audioSource = GetComponent<AudioSource>();
    }
    #region//1ステージでのアイテムの獲得動作
    public void OnItem0()
    {
        BoxItem0.SetActive(true);
        BoxItem1.SetActive(false);
        BoxItem2.SetActive(false);
        BoxItem3.SetActive(false);
        Doku = true;
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem1()
    {
        BoxItem0.SetActive(false);
        BoxItem1.SetActive(true);
        BoxItem2.SetActive(false);
        BoxItem3.SetActive(false);
        Doku = false; 
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem2()
    {
        BoxItem0.SetActive(false);
        BoxItem1.SetActive(false);
        BoxItem2.SetActive(true);
        BoxItem3.SetActive(false);
        Doku = false;
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem3()
    {
        BoxItem0.SetActive(false);
        BoxItem1.SetActive(false);
        BoxItem2.SetActive(false);
        BoxItem3.SetActive(true);
        Doku = false;
        audioSource.PlayOneShot(sound2);

    }
    #endregion
    #region//2ステージでのアイテムの獲得動作
    public void OnItem4()
    {
        BoxItem4.SetActive(true);
        BoxItem5.SetActive(false);
        BoxItem6.SetActive(false);
        BoxItem7.SetActive(false);
        Kiss = true;
        Nuno = false;
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem5()
    {
        BoxItem4.SetActive(false);
        BoxItem5.SetActive(true);
        BoxItem6.SetActive(false);
        BoxItem7.SetActive(false);
        Kiss = false;
        Nuno = true;
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem6()
    {
        BoxItem4.SetActive(false);
        BoxItem5.SetActive(false);
        BoxItem6.SetActive(true);
        BoxItem7.SetActive(false);
        Kiss = false;
        Nuno = false;
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem7()
    {
        BoxItem4.SetActive(false);
        BoxItem5.SetActive(false);
        BoxItem6.SetActive(false);
        BoxItem7.SetActive(true);
        Kiss = false;
        Nuno = false;
        audioSource.PlayOneShot(sound2);

    }
    #endregion
    #region//3ステージでのアイテムの獲得動作
    public void OnItem8()
    {
        BoxItem8.SetActive(true);
        BoxItem9.SetActive(false);
        BoxItem10.SetActive(false);
        BoxItem11.SetActive(false);
        Unchi = true;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem9()
    {
        BoxItem8.SetActive(false);
        BoxItem9.SetActive(true);
        BoxItem10.SetActive(false);
        BoxItem11.SetActive(false);
        Unchi = false;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem10()
    {
        BoxItem8.SetActive(false);
        BoxItem9.SetActive(false);
        BoxItem10.SetActive(true);
        BoxItem11.SetActive(false);
        Unchi = false;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem11()
    {
        BoxItem8.SetActive(false);
        BoxItem9.SetActive(false);
        BoxItem10.SetActive(false);
        BoxItem11.SetActive(true);
        Unchi = false;
        audioSource.PlayOneShot(sound2);
    }
    #endregion
    #region//4ステージでのアイテムの獲得動作
    public void OnItem12()
    {
        BoxItem12.SetActive(true);
        BoxItem13.SetActive(false);
        BoxItem14.SetActive(false);
        BoxItem15.SetActive(false);
        Te = true;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem13()
    {
        BoxItem12.SetActive(false);
        BoxItem13.SetActive(true);
        BoxItem14.SetActive(false);
        BoxItem15.SetActive(false);
        Te = false;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem14()
    {
        BoxItem12.SetActive(false);
        BoxItem13.SetActive(false);
        BoxItem14.SetActive(true);
        BoxItem15.SetActive(false);
        Te = false;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem15()
    {
        BoxItem12.SetActive(false);
        BoxItem13.SetActive(false);
        BoxItem14.SetActive(false);
        BoxItem15.SetActive(true);
        Te = false;
        audioSource.PlayOneShot(sound2);
    }
    #endregion
    #region//5ステージでのアイテムの獲得動作
    public void OnItem16()
    {
        BoxItem16.SetActive(true);
        BoxItem17.SetActive(false);
        BoxItem18.SetActive(false);
        BoxItem19.SetActive(false);
        Heart = true;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem17()
    {
        BoxItem16.SetActive(false);
        BoxItem17.SetActive(true);
        BoxItem18.SetActive(false);
        BoxItem19.SetActive(false);
        Heart = false;
        audioSource.PlayOneShot(sound2);
    }
    public void OnItem18()
    {
        BoxItem16.SetActive(false);
        BoxItem17.SetActive(false);
        BoxItem18.SetActive(true);
        BoxItem19.SetActive(false);
        Heart = false;
        audioSource.PlayOneShot(sound2);

    }
    public void OnItem19()
    {
        BoxItem16.SetActive(false);
        BoxItem17.SetActive(false);
        BoxItem18.SetActive(false);
        BoxItem19.SetActive(true);
        Heart = false;
        audioSource.PlayOneShot(sound2);

    }
    #endregion


    #region//犯人たちの集まり
    //アンパンマン
    public void OnAnpan()
    {
        if (Doku == true)
        {
            this.transform.localPosition = new Vector2(-1000, 1000);
            Debug.Log("idou");
            audioSource.PlayOneShot(sound3);
        }
        else
        {
            //ぶぶーと効果音を出す

        }
    }
    //イワンコフ
    public void OnIwankohu()
    {
        if (Kiss == true)
        {
            this.transform.localPosition = new Vector2(-2000, 2000);
            Debug.Log("idou");
            audioSource.PlayOneShot(sound3);
        }
        else
        {
            //ぶぶーと効果音を出す

        }
    }
    //ルフィ
    public void OnRufi()
    {
        if(Nuno==true)
        {
            this.transform.localPosition = new Vector2(-2000, 3000);
          
        }
    }
    //しんのすけ
    public void OnSinnosuke()
    {
        if (Unchi == true)
        {
            this.transform.localPosition = new Vector2(-3000, 1000);
            Debug.Log("idou");
            audioSource.PlayOneShot(sound3);
        }
        else
        {
            //ぶぶーと効果音を出す

        }
    }
    //テンシンハン
    public void OnTensinhan()
    {
        if (Te == true)
        {
            this.transform.localPosition = new Vector2(-4000, 1000);
            Debug.Log("idou");
            audioSource.PlayOneShot(sound3);
        }
        else
        {
            //ぶぶーと効果音を出す

        }
    }
    //ルパン三世
    public void OnRupansansei()
    {
        if (Heart == true)
        {
            this.transform.localPosition = new Vector2(-5000, 1000);
            Debug.Log("idou");
            audioSource.PlayOneShot(sound3);
        }
        else
        {
            //ぶぶーと効果音を出す

        }
    }

        #endregion

    }



    