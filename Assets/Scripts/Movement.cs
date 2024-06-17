using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speedX;

    public float inputX;

    public float speedY = 0;

    public float jumpSpeed = 10f;

    public float gravity = 9.8f;

    public float platformY = 0F;

    public int jumpCount = 0;

    public bool isJump => anim.GetBool("IsJump");

    public bool isFall => anim.GetBool("IsFall");

    public bool isRun => anim.GetBool("IsRun");

    public Shadow shadow;

    public bool canFall;

    public Platform nowPlatform;

    private PlatformLogic platformLogic;

    private Animator anim;

    public Stack<HistoryInfo> historyStack = new Stack<HistoryInfo>();

    public bool isTracing;

    public AudioSource backMusic;

    public bool canTrace = true;
    


    private void Awake()
    {
        EventCenter.Instance.AddEventListener<float>(E_EventType.E_Input_Horizontal, Run);

        InputMgr.Instance.ChangeKeyboardInfo(E_EventType.Jump, KeyCode.Space, InputInfo.E_InputType.Down);
        EventCenter.Instance.AddEventListener(E_EventType.Jump, Jump);

        InputMgr.Instance.ChangeKeyboardInfo(E_EventType.PlatformFall, KeyCode.DownArrow, InputInfo.E_InputType.Down);
        EventCenter.Instance.AddEventListener(E_EventType.PlatformFall, FallFromPlatform);

        InputMgr.Instance.ChangeKeyboardInfo(E_EventType.TraceBack, KeyCode.LeftShift, InputInfo.E_InputType.Always);
        EventCenter.Instance.AddEventListener(E_EventType.TraceBack, TraceBack);

        InputMgr.Instance.ChangeKeyboardInfo(E_EventType.StopTraceBack, KeyCode.LeftShift, InputInfo.E_InputType.Up);
        EventCenter.Instance.AddEventListener(E_EventType.StopTraceBack, StopTraceBack);


        platformLogic = new PlatformLogic(this);
    }

 
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        shadow.ShadowScale(transform.position.y - platformY);
        shadow.ShadowMove(new(transform.position.x, platformY, 0.35f));


        #region ÌøÔ¾
        if (isJump || isFall || speedY != 0 && !isTracing )
        {

            speedY -= gravity * Time.deltaTime;
            transform.Translate(Vector3.up * speedY * Time.deltaTime);

            if (transform.position.y <= platformY)
            {
                transform.position = new Vector3(transform.position.x, platformY, transform.position.z);
                anim.SetBool("IsFall", false);
                anim.SetBool("IsJump", false);
                speedY = 0;
                jumpCount = 0;
            }
                anim.SetBool("IsFall", speedY < 0);
        }

        platformLogic.UpdateCheck();

        #endregion

        if (isTracing)
        {
            return;
        }

        #region ÒÆ¶¯
        if (inputX != 0)
        {

            anim.SetBool("IsRun", true);
            transform.Translate(Vector3.right * speedX * inputX * Time.deltaTime, Space.World);

            if (inputX > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90f, 0);
            }
            else if (inputX < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90f, 0);
            }
        }
        else
        {
            anim.SetBool("IsRun", false);
        }
        #endregion
        Record();
    }

    void Run(float inputX)
    {
        if(!isTracing)
        this.inputX = inputX;
    }

    void Jump()
    {
        if(jumpCount < 2 && !isTracing)
        {
            anim.SetBool("IsJump", true);
            speedY = jumpSpeed;
            jumpCount++;
        }
    }

    public void Fall()
    {
        anim.SetBool("IsFall", true);
        platformY = -999999f;
    }

    public void  FallFromPlatform()
    {
         if(canFall && !isFall && !isJump)
        {
            Fall();
        }
     
    }

    public void ChangeNowPlatform(Platform nowPlatform)
    {
        this.nowPlatform = nowPlatform;
        platformY = nowPlatform.y;
        canFall = nowPlatform.canFall;
        shadow.gameObject.SetActive(nowPlatform.canShowShadow);
    }

    void Record()
    {
        HistoryInfo info = new HistoryInfo();
        info.postion = transform.position;
        info.rotation = transform.rotation;
        info.inputX = -inputX;
        info.speedY = -speedY;
        info.jumpCount = jumpCount;
        info.isJump = isJump;
        info.isFall = isFall;
        info.isRun = isRun;
        info.nowPlatform = nowPlatform;
        info.animStateInfo0 = anim.GetCurrentAnimatorStateInfo(0);
        info.animStateInfo1 = anim.GetCurrentAnimatorStateInfo(1);
        historyStack.Push(info);
     
    }

    void TraceBack()
    {
        if(!canTrace)
        {
            return;
        }

        if(historyStack.Count != 0)
        {

            anim.SetBool("IsTracing", true);
            isTracing = true;
            HistoryInfo info = historyStack.Pop();
            transform.position = info.postion;
            transform.rotation = info.rotation;
            inputX = info.inputX;
            speedY = info.speedY;
            jumpCount = info.jumpCount;
            nowPlatform = info.nowPlatform;
            platformY = nowPlatform == null ? 0 : nowPlatform.y;
            canFall = nowPlatform == null ? false : nowPlatform.canFall;
            shadow.gameObject.SetActive(nowPlatform == null? true : nowPlatform.canShowShadow);
            platformLogic.nowPlatform = nowPlatform;
            anim.SetBool("IsFall", info.isFall);
            anim.SetBool("IsJump", info.isJump);

            anim.Play(info.animStateInfo0.shortNameHash, 0, info.animStateInfo0.normalizedTime % 1);
           
            anim.Play(info.animStateInfo1.shortNameHash, 1, info.animStateInfo1.normalizedTime % 1);
            
            backMusic.pitch = -1;
        }
        else
        {
            anim.SetBool("IsTracing", false);
            isTracing = false;
            backMusic.pitch = 1;
            canTrace = false;
        }
    }

    void StopTraceBack()
    {
        anim.SetBool("IsTracing", false);
        isTracing = false;
        backMusic.pitch = 1;
        canTrace = true;
    }
}
