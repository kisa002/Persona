using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    //Main
    public int menu = 0;

    GameObject main;
    GameObject select;
    GameObject credit;
    GameObject textCredit;

    GameObject logo;

    int shakeDir = -1;
    
    //Gauge
    Gauge[] gauge = new Gauge[4];

    public float[] value = new float[4];
    static public float gaugeAll = 0;


    //Timer
    public int timer = 61;
    Text textTimer;

    int countdown = 3;
    Image imageCountdown;
    Image imageTimerOver;


    //Result
    public bool isFinish = false;

    GameObject result;
    GameObject panelResult;
    GameObject resultAlpha;
    GameObject[] resultPlayer = new GameObject[4];
    Image imagePlayer;

    public int[] rank = new int[4];

    void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            main = GameObject.Find("Main");
            select = GameObject.Find("Select");
            credit = GameObject.Find("Credit");

            logo = GameObject.Find("Logo");

            select.active = false;
            credit.active = false;

            StartCoroutine(ShakeLogo());
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            textTimer = GameObject.Find("TextTimer").GetComponent<Text>();
            //StartCoroutine(Timer());

            imageCountdown = GameObject.Find("ImageCountdown").GetComponent<Image>();
            StartCoroutine(Countdown());

            imageTimerOver = GameObject.Find("ImageTimerOver").GetComponent<Image>();
            imageTimerOver.enabled = false;

            GameObject resultAlpha = GameObject.Find("BackgroundAlpha");
            //resultAlpha.active = false;

            imageCountdown = GameObject.Find("ImageCountdown").GetComponent<Image>();

            imagePlayer = GameObject.Find("ImagePlayer").GetComponent<Image>();

            for (int i = 0; i < 4; i++)
            {
                gauge[i] = new Gauge();

                gauge[i].obj = GameObject.Find("Gauge" + (i + 1));
                gauge[i].textPercent = gauge[i].obj.transform.FindChild("TextPercent").GetComponent<Text>();
            }

            result = GameObject.Find("Result");
            panelResult = GameObject.Find("PanelResult");

            for (int i = 0; i < 4; i++)
            {
                resultPlayer[i] = panelResult.transform.FindChild("Player" + (i + 1)).gameObject;
                resultPlayer[i].transform.SetParent(GameObject.Find("Result").transform.FindChild("Hide"));
            }

            panelResult.active = false;
            result.active = false;
        }
	}
	
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if(!isFinish)
            {
                CheckGauge();

                for (int i = 0; i < 4; i++)
                {
                    gauge[i].value = value[i];
                    gauge[i].ChangeWidth();

                    if (i == 0)
                        gauge[i].obj.transform.position = new Vector2(0, 1000f);
                    else
                        gauge[i].obj.transform.position = new Vector2(gauge[i - 1].obj.transform.position.x + gauge[i - 1].obj.GetComponent<RectTransform>().rect.width, 1000f);
                }
            }
            else
            {
                if(!panelResult.active)
                {
                    result.active = true;
                    panelResult.active = true;
                    //resultAlpha.active = true;

                    for (int i = 0; i < 4; i++)
                    {
                        rank[i] = 0;

                        for (int j = 0; j < 4; j++)
                        {
                            if (gauge[i].percent < gauge[j].percent)
                                rank[i]++;
                        }
                    }

                    int count = 0;

                    for(int i=0; i<4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (rank[j] == count)
                            {
                                resultPlayer[j].transform.SetParent(panelResult.transform);
                                resultPlayer[j].transform.FindChild("Rank").transform.FindChild("Text").GetComponent<Text>().text = (count + 1).ToString();

                                if (count == 0)
                                    imagePlayer.sprite = Resources.Load<Sprite>("Sprites/" + (j + 1) + "p");

                                break;
                            }
                        }

                        if (count == 3)
                            break;

                        count++;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        resultPlayer[i].transform.FindChild("Gauge").transform.FindChild("TextPercent").GetComponent<Text>().text = Mathf.Round(gauge[i].percent).ToString() + "%";

                        gauge[i].obj = resultPlayer[i].transform.FindChild("Gauge").gameObject;
                        gauge[i].ResultWidth();
                    }
                }
            }
        }

        if(SceneManager.GetActiveScene().name == "Main")
        {
            //if (credit.active)
            //{
            //    textCredit.transform.Translate(Vector2.up * 200f * Time.deltaTime);

            //    if (textCredit.transform.position.y >= 1300)
            //    {
            //        textCredit.transform.position = new Vector2(textCredit.transform.position.x, -20f);

            //        ShowMain();
            //    }
            //}

            if (Input.GetKeyDown(KeyCode.Escape))
                ShowMain();
        }
    }

    public void ShowMain()
    {
        select.active = false;
        credit.active = false;

        main.active = true;
    }

    public void ShowSelect()
    {
        StartCoroutine(ChangeMenu(1));

        main.active = false;
        credit.active = false;

        select.active = true;
    }

    public void ShowCredit()
    {
        credit.active = true;

        main.active = false;
        select.active = false;
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator Timer()
    {
        timer -= 1;
        yield return new WaitForSeconds(1f);

        if (timer >= 10)
            textTimer.text = (timer.ToString()).Substring(0, 1) + " " + (timer.ToString()).Substring(1, 1);
        else
            textTimer.text = timer.ToString();

        if (timer <= 0)
            StartCoroutine(TimeOver());
        else
            StartCoroutine(Timer());
    }

    void CheckGauge()
    {
        gaugeAll = gauge[0].value + gauge[1].value + gauge[2].value + gauge[3].value;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        SceneManager.LoadScene("Main");
    }

    class Gauge
    {
        public GameObject obj;
        public Text textPercent;

        public float value = 0;

        public float percent = 0;

        public void ChangeWidth()
        {
            if(gaugeAll > 0)
            {
                percent = (value / gaugeAll) * 100f;

                obj.GetComponent<RectTransform>().sizeDelta = new Vector2((percent / 100f) * 1920f, obj.GetComponent<RectTransform>().rect.height);
            }

            textPercent.text = Mathf.Round(percent).ToString() + "%";
        }

        public void ResultWidth()
        {
            if (gaugeAll > 0)
            {
                percent = (value / gaugeAll) * 100f;

                obj.GetComponent<RectTransform>().sizeDelta = new Vector2((percent / 100f) * 1000f, obj.GetComponent<RectTransform>().rect.height);
            }
        }
    }

    IEnumerator ActiveShow(GameObject obj, bool check)
    {
        yield return new WaitForSeconds(0.5f);

        obj.active = check;
    }

    IEnumerator ChangeMenu(int value)
    {
        yield return new WaitForSeconds(0.5f);

        menu = value;
    }

    IEnumerator ShakeLogo()
    {
        shakeDir *= -1;

        for (int i = 0; i < 60; i++)
        {
            //logo.transform.Rotate(0f, 0f, (0.15f * shakeDir));
            logo.transform.localScale += new Vector3(0.002f * shakeDir, 0.002f * shakeDir, 0f);
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(ShakeLogo());
    }

    IEnumerator Countdown()
    {
        if(countdown != 0)
            imageCountdown.sprite = Resources.Load<Sprite>("Sprites/" + countdown);
        else
        {
            imageCountdown.sprite = Resources.Load<Sprite>("Sprites/start");

            imageCountdown.GetComponent<RectTransform>().sizeDelta = new Vector2(871, 314);
        }

        yield return new WaitForSeconds(1f);

        countdown--;

        if (countdown >= 0)
            StartCoroutine(Countdown());
        else
        {
            GameObject.Find("ImageCountdown").active = false;
            StartCoroutine(Timer());
        }
    }

    IEnumerator TimeOver()
    {
        imageTimerOver.enabled = true;

        for (int i = 0; i < 25; i++)
        {
            imageTimerOver.color = new Color(imageTimerOver.color.r, imageTimerOver.color.g, imageTimerOver.color.b, i * 0.04f);
            imageTimerOver.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(6 * (i*4), 5.3f * (i*4));
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(2f);

        isFinish = true;
        imageTimerOver.enabled = false;
    }
}