using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zako1 : MonoBehaviour
{
    [Header("���Z�X�R�A")] public int myScore;
    [Header("�ړ����x")] public float speed;
    [Header("��ʊO�ł��s�����邩")] public bool nonVisible;
    [Header("�d��")] public float gravity;
    [Header("�ڐG����")] public EnemyCollision checkCollision;

    //�v���C�x�[�g�ϐ�
    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private CapsuleCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!oc.playerStepOn)
        { 
        if (sr.isVisible || nonVisible)
        {
                if(checkCollision.isOn)
                {
                    rightTleftF = !rightTleftF;
                }

            //�s������
            int xVector = -1;
            if (rightTleftF)
            {
                xVector = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            rb.velocity = new Vector2(xVector * speed, -gravity);
        }
        else
        {
            rb.Sleep();
        }
        }
        else
        {
            if(!isDead)
            {
                anim.Play("dead");
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                if (GManager.instance != null)
                {
                    GManager.instance.score += myScore;
                }
                Destroy(gameObject, 3f);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5));
            }
        }
    }
}
