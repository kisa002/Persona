using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerColor : MonoBehaviour
{
    public GameObject brush;
    public GameObject bucket;
    public Collider feet;

    private Material brushMat;
    private Material bucketMat;
    private BasePlayer bp;

    void Start ()
    {
        brushMat = brush.GetComponent<Renderer>().materials[1];
        bucketMat = bucket.GetComponent<Renderer>().materials[1];
        bp = gameObject.GetComponent<BasePlayer>();

        brushMat.color = PlayerColorManager.GetColor(bp.originalColor);
        bucketMat.color = PlayerColorManager.GetColor(bp.originalColor);

        bucket.SetActive(false);
        feet.enabled = false;
    }

	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision");
        if(other.CompareTag("PaintBucketThrowed"))
        {
            bp.anim.SetTrigger("HitByBucket");
            bp.canMove = false;
            bp.x = bp.z = 0f;

            bp.brushColor = other.GetComponent<PaintBucketThrowedBehaviour>().color;
            brushMat.color  = PlayerColorManager.GetColor(bp.brushColor);
            if(bp.brushColor != bp.originalColor)
            {
                feet.enabled = true;
            }
            else
            {
                feet.enabled = false;
            }
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("PaintBucket"))
        {
            bp.PlaySound(12);
            if(bucket.activeSelf == false)
            {
                bucket.SetActive(true);
            }            
            bp.bucketColor = other.GetComponent<PaintBucketBehaviour>().color;
            bucketMat.color = PlayerColorManager.GetColor(bp.bucketColor);

            if(bp.bucketColor == bp.originalColor)
            {
                feet.enabled = false;
                brushMat.color = bucketMat.color;
                bp.brushColor = bp.bucketColor;
            }
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("AttackArea"))
        {
            bp.anim.SetTrigger("HitByBrush");
            bp.canMove = false;
            bp.x = bp.z = 0f;
        }
    }
}
