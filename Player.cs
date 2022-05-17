using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRei : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ肷��
    [Header("���x")]public float speed;//���x
    [Header("�d��")] public float gravity;//�d��
    [Header("�W�����v�̍���")] public float jumpHight;//�W�����v�̍���
    [Header("�W�����v�̑��x")] public float jumpSpeed;//�W�����v�̑��x
    [Header("�W�����v��������")] public float jumpLimitTime;//�W�����v��������
    [Header("���݂�����̍����̊���")] public float stepOnRate;//���݂�����̍����̊���
    [Header("�ڒn����")] public GroundCheck ground;//�ڒn����
    [Header("�����Ԃ�������")] public GroundCheck head;//�����Ԃ�������
    [Header("�_�b�V���̑����\��")] public AnimationCurve dashCurve;
    [Header("�W�����v�̑����\��")] public AnimationCurve jumpCurve;
    [Header("�W�����v����Ƃ��ɖ炷")] public AudioClip jumpSE;//�W�����v����Ƃ��ɖ炷


    //�v���C�x�[�g�ϐ�
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
        //�R���|�[�l���g�̃C���X�^���X��߂܂���
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
            //���Ł@���Ă���Ƃ��߂�
            if (blirkTime > 0.2f)
            {
                sr.enabled = true;
                blirkTime = 0.0f;
            }
            //���Ł@�����Ă���Ƃ�
            else if(blirkTime>0.1f)
            {
                sr.enabled = false;
            }
            //���Ł@���Ă���Ƃ�
            else
            {
                sr.enabled = true;
            }
          
            //�P�b�������疾�ŏI���
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
            //�ڒn����𓾂�
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //�e���W���̑��x�����߂�
            float ySpeed = GetYSpeed();
            float xSpeed = GetXSpeed();

            //�A�j���[�V������K�p
            SetAnimation();

            //�ړ����x��ݒ�
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, -gravity);
        }

    }

    /// <summary>
    /// �������ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    private float GetYSpeed()
    {
        float verticalKey = Input.GetAxis("Vertical");//�����ɃX�}�z�ł̓���������
        float ySpeed = -gravity;

        if (isOtherJump)
        {
            //��{�^����������Ă��邩�A���݂̍������W�����v�����ʒu���玩���̂����W��艺

            bool canHight = jumpPos + otherJumpHeight > transform.position.y;//���݂̍�������ׂ鍂����艺��
            bool canTime = jumpLimitTime > jumpTime;//�W�����v���Ԃ������Ȃ肷���Ă��Ȃ���

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
        //�n�ʂɂ��Ă���Ƃ�
          else  if (isGround)
        {
            if (verticalKey > 0|| Input.GetMouseButtonDown(0))
            {
               if (!isJump)
                {
                   GManager.instance.PlaySE(jumpSE);
                }
                isJump = true;
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;//�W�����v�����������L�^����
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //��{�^����������Ă��邩�A���݂̍������W�����v�����ʒu���玩���̂����W��艺

            bool pushUpKey = verticalKey > 0;//������L�[�������Ă��邩�ǂ���
            bool canHight = jumpPos + jumpHight > transform.position.y;//���݂̍�������ׂ鍂����艺��
            bool canTime = jumpLimitTime > jumpTime;//�W�����v���Ԃ������Ȃ肷���Ă��Ȃ���

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
            //�A�j���[�V�����J�[�u�𑬓x�ɓK�p
            if (isJump||isOtherJump)
            {
                ySpeed *= jumpCurve.Evaluate(jumpTime);
            }
            
        }
        return ySpeed;
    }

    private float GetXSpeed()
    {
        //�R���|�[�l���g�̃C���X�^���X��߂܂���
        float horizontalKey = Input.GetAxis("Horizontal");//�����ɃX�}�z�̓�����������

        //�L�[���͂��ꂽ��s������

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

        //�O��̓��͂���_�b�V���̔��]�𔻒f���đ��x��ς���
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
    /// �R���e�B�j���[�ҋ@��Ԃ�
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

    //�_�E���A�j���[�V�������������Ă��邩�ǂ���
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
    /// �R���e�B�j���[����
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
                //�_�E������
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
        //�ڐG����
        private void OnCollisionEnter2D(Collision2D collision)
        {
        
        if (collision.collider.tag == enemyTag)
        {
            //���݂�����̍���
            float stepOnHeight = (capool.size.y * (stepOnRate / 100f));

            //���݂�����̃��[���h���W
            float judgePos = transform.position.y - (capool.size.y / 2f) + stepOnHeight;

            foreach (ContactPoint2D p in collision.contacts)
            {
  

                if (p.point.y < judgePos)
                {
                    //������x���˂�
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                    if(o!=null)
                    {
                        GManager.instance.PlaySE(jumpSE);

                        otherJumpHeight = o.bourdHeight;//����Â��������璵�˂鍂�����擾����
                        o.playerStepOn = true;//����Â������ɑ΂��ē���Â������Ƃ�ʒm����
                        jumpPos = transform.position.y;//�W�����v�����ʒu���L�^����
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                    else
                    {
                        Debug.Log("ObjectCollision�����Ă��Ȃ���");
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
