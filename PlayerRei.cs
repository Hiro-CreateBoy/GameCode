using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定する
    public float speed;//速度
    public float gravity;//重力
    public float jumpHight;//ジャンプの高さ
    public float jumpSpeed;//ジャンプの速度
    public float jumpLimitTime;//ジャンプ制限時間
    public float stepOnRate;//踏みつけ判定の高さの割合
    public GroundCheck ground;//接地判定
    public GroundCheck head;//頭をぶつけた判定
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;


    //プライベート変数
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capool = null;
    private SpriteRenderer sr = null;
    private bool isHead = false;
    private bool isGround = false;
    private bool isJump = false;
    private bool isRun = false;
    private bool isDown = false;
    private bool isOtherJump = false;
    private bool isContinue = false;
    private bool nonDownAnim = false;
    private float continueTime = 0.0f;
    private float blirkTime = 0.0f;
    private float jumpPos = 0.0f;
    private float otherJumpHeight = 0.0f;
    private float jumpTime = 0.0f;
    private float dashTime = 0.0f;
    private float beforeKey = 0.0f;
    private string enemyTag = "Enemy";
    private string daedAreaTag = "DeadArea";
    private string hitAreaTag = "HitArea";

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントのインスタンスを捕まえる
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capool = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(isContinue)
        {
            //明滅　ついているとき戻る
            if (blirkTime > 0.2f)
            {
                sr.enabled = true;
                blirkTime = 0.0f;
            }
            //明滅　消えているとき
            else if(blirkTime>0.1f)
            {
                sr.enabled = false;
            }
            //明滅　ついているとき
            else
            {
                sr.enabled = true;
            }

            //１秒たったら明滅終わり
            if(continueTime>1.0f)
            {
                isContinue = false;
                blirkTime = 0.0f;
                continueTime = 0.0f;
                sr.enabled = true;

            }
            else
            {
                blirkTime += Time.deltaTime;
                continueTime += Time.deltaTime;
            }
        }
        


    }

    void FixedUpdate()
    {
        if (!isDown&&!GManager.instance.isGameOver)
        {
            //接地判定を得る
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //各座標軸の速度を求める
            float ySpeed = GetYSpeed();
            float xSpeed = GetXSpeed();

            //アニメーションを適用
            SetAnimation();

            //移動速度を設定
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, -gravity);
        }

    }

    /// <summary>
    /// ｙ成分で必要な計算をし、速度を返す
    /// </summary>
    private float GetYSpeed()
    {
        float verticalKey = Input.GetAxis("Vertical");//ここにスマホでの動きを入れる
        float ySpeed = -gravity;

        if (isOtherJump)
        {
            //上ボタンが押されているかつ、現在の高さがジャンプした位置から自分のｙ座標より下

            bool canHight = jumpPos + otherJumpHeight > transform.position.y;//現在の高さが飛べる高さより下か
            bool canTime = jumpLimitTime > jumpTime;//ジャンプ時間が長くなりすぎていないか

            if (canHight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isOtherJump = false;
                jumpTime = 0.0f;
            }
        }
        //地面についているとき
          else  if (isGround)
        {
            if (verticalKey > 0)
            {
                isJump = true;
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;//ジャンプした高さを記録する
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //上ボタンが押されているかつ、現在の高さがジャンプした位置から自分のｙ座標より下

            bool pushUpKey = verticalKey > 0;//上方向キーを押しているかどうか
            bool canHight = jumpPos + jumpHight > transform.position.y;//現在の高さが飛べる高さより下か
            bool canTime = jumpLimitTime > jumpTime;//ジャンプ時間が長くなりすぎていないか

            if (pushUpKey && canHight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
            //アニメーションカーブを速度に適用
            if (isJump||isOtherJump)
            {
                ySpeed *= jumpCurve.Evaluate(jumpTime);
            }
            
        }
        return ySpeed;
    }

    private float GetXSpeed()
    {
        //コンポーネントのインスタンスを捕まえる
        float horizontalKey = Input.GetAxis("Horizontal");//ここにスマホの動きを加える

        //キー入力されたら行動する

        float xSpeed = 0.0f;

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;

            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        //前回の入力からダッシュの反転を判断して速度を変える
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;


        xSpeed *= dashCurve.Evaluate(dashTime);
        return xSpeed;
    }

    private void SetAnimation()
    {
        anim.SetBool("jump", isJump||isOtherJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }

    /// <summary>
    /// コンティニュー待機状態か
    /// </summary>
    /// <returns></returns>
    public bool IsContinueWaiting()
    {
        if (GManager.instance.isGameOver)
        {
            return false;
        }
        else
        {
            return IsDownAnimEnd()||nonDownAnim;
        }
    }

    //ダウンアニメーションが完了しているかどうか
    private bool IsDownAnimEnd()
    {
        if (isDown && anim != null)
        {
            AnimatorStateInfo currrentState = anim.GetCurrentAnimatorStateInfo(0);
            if(currrentState.IsName("player_deth"))
            {
                if(currrentState.normalizedTime>=1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// コンティニューする
    /// </summary>
    public void ContinuePlayer()
    {
        isDown = false;
        anim.Play("player_stand");
        isJump = false;
        isOtherJump = false;
        isRun = false;
        isContinue = true;
        nonDownAnim = false;
    }

    private void ReceiveDamage(bool downAnim)
    {
        if (isDown)
        {
            return;
        }
        else 
        {
            if (downAnim)
            {
                //ダウンする
                anim.Play("player_deth");
            }
            else
            {
                nonDownAnim = true;
            }
                isDown = true;
                GManager.instance.SubHeartNum();
            
        }

    }
        //接触判定
        private void OnCollisionEnter2D(Collision2D collision)
        {
        
        if (collision.collider.tag == enemyTag)
        {
            //踏みつけ判定の高さ
            float stepOnHeight = (capool.size.y * (stepOnRate / 100f));

            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capool.size.y / 2f) + stepOnHeight;

            foreach (ContactPoint2D p in collision.contacts)
            {
  

                if (p.point.y < judgePos)
                {
                    //もう一度跳ねる
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                    if(o!=null)
                    {
                        otherJumpHeight = o.bourdHeight;//踏んづけた物から跳ねる高さを取得する
                        o.playerStepOn = true;//踏んづけた物に対して踏んづけたことを通知する
                        jumpPos = transform.position.y;//ジャンプした位置を記録する
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                    else
                    {
                        Debug.Log("ObjectCollisionがついていないよ");
                    }
                }
                else
                {
                    ReceiveDamage(true);
                    break;
                }
            }
        }
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== daedAreaTag)
        {
            ReceiveDamage(false);
        }
        else if(collision.tag==hitAreaTag)
        {
            ReceiveDamage(true);
        }
    }
}
