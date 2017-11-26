using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public enum PlayerIndex { Unknown = -1,One, Two, Three, Four};
    public enum PlayerState { eIdle, eWalk, eDash, eDie};

    public PlayerState playerState {
        get {
            return _playerState;
        }
        set {
            if(value == PlayerState.eWalk) {
                anim.SetBool("IsWalking", true);
            }
            else {
                anim.SetBool("IsWalking", false);
            }
            
            if(value == PlayerState.eDash) {
                anim.SetTrigger("Dash");
                canMove = false;
                coll.enabled = false;
                PlayerMovePower = PlayerDashPower;
                x = transform.TransformDirection(Vector3.back).x;
                z = transform.TransformDirection(Vector3.back).z;
            }

            if(value == PlayerState.eDie) {
                anim.SetTrigger("Die");
            }

            _playerState = value;
        }
    }

    public List<KeyCode> keyCodeList = new List<KeyCode>();
    public GameObject bucket;
    public GameObject bucketThrowed;
    public Collider attackArea;
    [HideInInspector]
    public Animator anim;
    public PlayerColor originalColor;
    public PlayerColor bucketColor;
    public PlayerColor brushColor;
    public PlayerIndex playerIndex;
    public float PlayerMovePowerBase = 3f;
    public float PlayerDashPower = 8f;
    public float PlayerMovePower = 3f;
    public float x = 0f;
    public float z = 0f;
    public bool canMove = true;

    public PlayerState _playerState = PlayerState.eIdle;

    private SoundManager sm;
    private Collider coll;
    private bool throwButtonUp = false;
    private bool dashButtonDown = false;
    private bool paintButtonUp = false;

    private string horizontal = "";
    private string vertical = "";

    // Use this for initialization
    void Start ()
    {
        GeneratorPlayer();
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        PlayerMovePower = PlayerMovePowerBase;
        bucketColor = originalColor;
        brushColor = originalColor;
        attackArea.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (canMove)
        {
            InputUpdate();
            StateUpdate();
        }
        PosUpdate();
    }

    public void InputUpdate()
    {
        x = Input.GetAxis(horizontal);
        z = Input.GetAxis(vertical);

        //Input 설정은 컨트롤러 오고 난 뒤에
        throwButtonUp = Input.GetKeyDown(keyCodeList[0]);
        dashButtonDown = Input.GetKeyDown(keyCodeList[1]);
        paintButtonUp = Input.GetKeyDown(keyCodeList[2]);
    }

    public void StateUpdate()
    {
        if (throwButtonUp && bucket.activeSelf)
        {
            anim.SetTrigger("Throw");
            StartThrow();
            throwButtonUp = false;
            attackArea.enabled = false;
        }
        else if (paintButtonUp)
        {
            anim.SetTrigger("Paint");
            paintButtonUp = false;
            attackArea.enabled = true;
        }

        if (dashButtonDown)
        {
            playerState = PlayerState.eDash;
            dashButtonDown = false;
            attackArea.enabled = false;
        }

        if (Mathf.Abs(x) <= float.Epsilon && Mathf.Abs(z) <= float.Epsilon)
        {
            playerState = PlayerState.eIdle;
        }
        else
        {
            playerState = PlayerState.eWalk;
        }
    }

    public void PosUpdate()
    {
        //이동할양
        Vector3 pos = new Vector3(x*PlayerMovePower, 0f, z* PlayerMovePower);
        //Debug.Log(pos);
        if (playerState != PlayerState.eIdle && !(Mathf.Abs(x) < float.Epsilon && Mathf.Abs(z) < float.Epsilon ))
        {
            transform.LookAt(transform.position + pos.normalized * PlayerMovePower);
            transform.Rotate(new Vector3(0, 180f, 0f));
        }
        transform.position += pos.normalized * PlayerMovePower;
    }

    public void DashEnd()
    {
        //Debug.Log("DashEnd");
        coll.enabled = true;
        canMove = true;
        PlayerMovePower = PlayerMovePowerBase;
    }

    public void PaintEnd()
    {
        attackArea.enabled = false;
    }

    public void CannotMoveEnd()
    {
        canMove = true;
    }

    public void StartThrow()
    {
        Vector3 direction = transform.TransformDirection(Vector3.back);
        Vector3 position = transform.position + direction * 100;
        position.y += 50;
        Quaternion rotation = transform.rotation;
        GameObject o = Instantiate(bucketThrowed, position, rotation);
        PaintBucketThrowedBehaviour b = o.GetComponent<PaintBucketThrowedBehaviour>();

        b.color = bucketColor;
        b.direction = direction;
        StartCoroutine(b.Throw());
        bucket.SetActive(false);
    }

    private void GeneratorPlayer()
    {
        switch (playerIndex)
        {
        case PlayerIndex.One:
            horizontal = "P1_Horizontal";
            vertical = "P1_Vertical";
            keyCodeList.Add(KeyCode.Joystick1Button2);
            keyCodeList.Add(KeyCode.Joystick1Button0);
            keyCodeList.Add(KeyCode.Joystick1Button1);
            break;
        case PlayerIndex.Two:
            horizontal = "P2_Horizontal";
            vertical = "P2_Vertical";
            keyCodeList.Add(KeyCode.Joystick2Button2);
            keyCodeList.Add(KeyCode.Joystick2Button0);
            keyCodeList.Add(KeyCode.Joystick2Button1);
            break;
        case PlayerIndex.Three:
            horizontal = "P3_Horizontal";
            vertical = "P3_Vertical";
            keyCodeList.Add(KeyCode.F);
            keyCodeList.Add(KeyCode.G);
            keyCodeList.Add(KeyCode.H);
            break;
        case PlayerIndex.Four:
            horizontal = "P4_Horizontal";
            vertical = "P4_Vertical";
            keyCodeList.Add(KeyCode.Keypad1);
            keyCodeList.Add(KeyCode.Keypad2);
            keyCodeList.Add(KeyCode.Keypad3);
            break;
        }
        //control = GetComponent<BasePlayerControl>();
    }

    public void BrushIndex(int index)
    {
        GetComponentInChildren<Brush>().Off(index-1);

        GetComponentInChildren<Brush>().On(index);
    }

    public void PlaySound(int index)
    {
        sm.PlaySound(index);
    }
}
