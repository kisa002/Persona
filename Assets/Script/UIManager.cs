using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public int timer = 60;
    Text textTimer;

    Gauge[] gauge = new Gauge[4];
    public float[] value = new float[4];
    static public float gaugeAll = 0;

    void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            textTimer = GameObject.Find("TextTimer").GetComponent<Text>();
            StartCoroutine(Timer());

            for (int i = 0; i < 4; i++)
            {
                gauge[i] = new Gauge();

                gauge[i].obj = GameObject.Find("Gauge" + (i + 1));
            }
        }
	}
	
	void Update () {
        CheckGauge();

		for(int i=0; i<4; i++)
        {
            gauge[i].value = value[i];
            gauge[i].ChangeWidth();

            if (i == 0)
            {
                gauge[i].obj.transform.position = new Vector2(0, 1000f);
                //Debug.Log("Gauge" + i + " - (POS)" + gauge[i].obj.transform.position + " / (WIDTH)" + gauge[i].obj.GetComponent<RectTransform>().rect.width);
            }
            else
            {
                gauge[i].obj.transform.position = new Vector2(gauge[i - 1].obj.transform.position.x + gauge[i - 1].obj.GetComponent<RectTransform>().rect.width, 1000f);
                Debug.Log("Gauge" + (i - 1) + " - (POS)" + gauge[i - 1].obj.transform.position + " / (WIDTH)" + gauge[i - 1].obj.GetComponent<RectTransform>().rect.width);
            }
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        timer -= 1;

        textTimer.text = timer.ToString();

        if(timer <= 0)
        {
            //TO DO ACTION
        }
        else
            StartCoroutine(Timer());
    }

    void CheckGauge()
    {
        gaugeAll = gauge[0].value + gauge[1].value + gauge[2].value + gauge[3].value;
    }

    class Gauge
    {
        public GameObject obj;
        public float value = 0;

        public float percent = 0;

        public void ChangeWidth()
        {
            if(gaugeAll > 0)
            {
                percent = (value / gaugeAll) * 100f;

                obj.GetComponent<RectTransform>().sizeDelta = new Vector2((percent / 100f) * 1920f, obj.GetComponent<RectTransform>().rect.height);

                //Debug.Log(obj.name + " : " + value + " / " + gaugeAll + " =  " + percent + "%");
            }
        }
    }
}