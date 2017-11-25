using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerControl : MonoBehaviour {

    public List<Vector2> dir;
    public List<float> rot;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRot(float h, float v)
    {
        Vector2 p = new Vector2(h, v);
        
        //for(int i = 0; i < 8; i++)
        //{
        //    if(dir[i].Equals(p))
        //    {
        //        GetComponent<Transform>().rotation = new Quaternion(0.0f, rot[i], 0.0f, 0.0f);
        //        break;
        //    }
        //}
    }
}
