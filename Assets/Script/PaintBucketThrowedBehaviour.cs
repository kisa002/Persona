using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketThrowedBehaviour : MonoBehaviour
{
    public GameObject child;
    public PlayerColor color;
    public float speed = 500f;
    public float remainTime = 3f;

    public Vector3 direction = Vector3.zero;

    void Start ()
    {
        child.GetComponent<Renderer>().materials[1].color = PlayerColorManager.GetColor(color);
        GetComponent<ObjectTeleporter>().area = GameObject.FindWithTag("StageArea").GetComponent<BoxCollider>();
    }

	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player collided");
    }

    public IEnumerator Throw()  //이 코루틴을 시작하기 전에 direction을 반드시 지정해 주세요
    {
        Debug.Log("Throw start");
        direction = direction.normalized;
        float startTime = Time.time;
        
        while (Time.time - startTime <= remainTime)
        {
            transform.position += direction * speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
