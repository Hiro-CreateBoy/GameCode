using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    private Text scoreText = null;
    private int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            scoreText.text = "×" + GManager.instance.heartNum;
        }
        else
        {
            Debug.Log("ゲームマネージャー置き忘れてるよ");
            Destroy(this);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldScore != GManager.instance.heartNum)
        {
            scoreText.text = "×" + GManager.instance.heartNum;
            oldScore = GManager.instance.heartNum;
        }
    }
}
