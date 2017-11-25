using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public List<Vector2> dir = new List<Vector2> {
        new Vector2(-1,0), new Vector2(1,0), new Vector2(0,1), new Vector2(0,-1),
        new Vector2(-1,1), new Vector2(1, 1), new Vector2(-1,-1), new Vector2(1,-1)
    };

    public List<float> rot = new List<float>
    {
        90.0f, 270.0f,180.0f,0.0f,
        135.0f, 225.0f, 45.0f, 315.0f
    };

    public enum PlayerIndex { One, Two, Three, Four};
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
                x = transform.TransformDirection(Vector3.left).x;
                z = transform.TransformDirection(Vector3.left).z;
            }

            if(value == PlayerState.eDie) {
                anim.SetTrigger("Die");
            }

            _playerState = value;
        }
    } 

    public PlayerIndex playerIndex;
    public float PlayerMovePowerBase = 3f;
    public float PlayerDashPower = 8f;
    public float PlayerMovePower = 3f;

    public PlayerState _playerState = PlayerState.eIdle;
    private Animator anim;
    private Collider coll;
    private float x = 0f;
    private float z = 0f;
    private bool throwButtonUp = false;
    private bool dashButtonDown = false;
    private bool paintButtonUp = false;

    private bool canMove = true;
    public Vector3 playerLook { get; private set; }

	// Use this for initialization
	void Start ()
    {
        GeneratorPlayer();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        PlayerMovePower = PlayerMovePowerBase;
        playerLook = Vector2.zero;
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
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
  

        //Input 설정은 컨트롤러 오고 난 뒤에
        throwButtonUp = Input.GetButtonUp("Throw");
        dashButtonDown = Input.GetButtonDown("Dash");
        paintButtonUp = Input.GetButtonUp("Paint");
    }

    public void StateUpdate()
    {
        if (throwButtonUp)
        {
            anim.SetTrigger("Throw");
        }
        else if (paintButtonUp)
        {
            anim.SetTrigger("Paint");
        }

        if (dashButtonDown)
        {
            playerState = PlayerState.eDash;
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
        Vector3 pos = new Vector3(x*PlayerMovePower, transform.position.y, z* PlayerMovePower);
        //Debug.Log(pos);
        if (playerState != PlayerState.eIdle)
        {
            transform.LookAt(transform.position + pos.normalized * PlayerMovePower);
            transform.Rotate(new Vector3(0, 90f, 0f));
            PlayerLookCheck();
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

    private void GeneratorPlayer()
    {
        Vector3 pos = new Vector3();

        switch (playerIndex)
        {
            case PlayerIndex.One:
                break;
            case PlayerIndex.Two:
                break;
            case PlayerIndex.Three:
                break;
            case PlayerIndex.Four:
                break;
        }
        //control = GetComponent<BasePlayerControl>();
    }

    private void PlayerLookCheck()
    {
        playerLook = transform.TransformDirection(Vector3.left);
        Debug.Log(playerLook);
    }
}
