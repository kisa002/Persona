using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour {

    public List<Collider> colliders;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Off(int index)
    {
        if(0 <= index && index < colliders.Capacity && colliders[index] != null)
            colliders[index].enabled = false;
    }

    public void On(int index)
    {
        if(0 <= index && index < colliders.Capacity && colliders[index] != null)
            colliders[index].enabled = true;
    }

    //public void SetOn(string isOn)
    //{
    //    foreach (var collider in colliders)
    //    {
    //        collider.enabled = bool.Parse(isOn);
    //    }
    //}
}
