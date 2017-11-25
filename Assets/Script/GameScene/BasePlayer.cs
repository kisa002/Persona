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

    public GameObject bucket;
    public GameObject bucketThrowed;
    public PlayerColor originalColor;
    public PlayerColor bucketColor;
    public PlayerColor brushColor;
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

	// Use this for initialization
	void Start ()
    {
        GeneratorPlayer();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        PlayerMovePower = PlayerMovePowerBase;
        bucketColor = originalColor;
        brushColor = originalColor;
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
        if (throwButtonUp && bucket.activeSelf)
        {
            anim.SetTrigger("Throw");
            StartThrow();
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

    public void BrushIndex(int index)
    {
        if(index != 0)
            GetComponentInChildren<Brush>().Off(index-1);

        GetComponentInChildren<Brush>().On(index);
    }
}
