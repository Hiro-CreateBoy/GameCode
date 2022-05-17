using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNum : MonoBehaviour
{
    private Text scoreText = null;
    private int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            scoreText.text = "Stage" + GManager.instance.stageNum;
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
        if (oldScore != GManager.instance.stageNum)
        {
            scoreText.text = "Stage" + GManager.instance.stageNum;
            oldScore = GManager.instance.stageNum;
        }
    }
}
