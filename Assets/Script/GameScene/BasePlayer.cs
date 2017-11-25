using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour {

    private BasePlayerControl control;

    public enum PlayerIndex { One, Two, Three, Four};
    public enum PlayerState { eIdel, eWalk, eDash, eDie};

    public PlayerIndex playerIndex;
    public float PlayerMovePowerBase;
    public float PlayerDashPower;

    public float PlayerMovePower;

	// Use this for initialization
	void Start () {
        GeneratorPlayer();

    }
	
	// Update is called once per frame
	void Update () {
        InputUpdate();
        StateUpdate();
    }

    public void InputUpdate()
    {
        if(Input.GetAxis("Jump") == 1.0f){
            PlayerMovePower = PlayerDashPower;
        }
        else{
            PlayerMovePower = PlayerMovePowerBase;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //control.SetRot(x, z);

        Vector3 pos = GetComponent<Transform>().position;

        pos.x += PlayerMovePower * x;
        pos.z += PlayerMovePower * z;
        GetComponent<Transform>().localPosition = pos;
    }

    public void StateUpdate()
    {

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
        control = GetComponent<BasePlayerControl>();
    }
}
