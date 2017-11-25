using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketThrowedBehaviour : MonoBehaviour
{
    public PlayerColor color;
    public float speed = 18f;
    public float remainTime = 3f;

    public Vector3 direction = Vector3.zero;

    void Start ()
    {
        gameObject.GetComponent<Renderer>().materials[1].color = PlayerColorManager.GetColor(color);

        StartCoroutine(Throw());
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
        direction = direction.normalized;
        float startTime = Time.time;

        while (Time.time - startTime <= remainTime)
        {
            transform.position += direction * speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject, 0.5f);
    }
}
