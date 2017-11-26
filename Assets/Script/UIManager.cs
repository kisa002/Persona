using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    
    public static int playerCount = 0;

    SoundManager soundManager;
    TitleMapGenerator generator;
    ItemSpawnManager spawner;
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
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        
        if (SceneManager.GetActiveScene().name == "Main")
        {
            main = GameObject.Find("Main");
            select = GameObject.Find("Select");
            credit = GameObject.Find("Credit");

            logo = GameObject.Find("Logo");

            select.SetActive(false);
            credit.SetActive(false);

            StartCoroutine(ShakeLogo());
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            generator = GameObject.Find("TileMapManager").GetComponent<TitleMapGenerator>();
            spawner = GameObject.FindGameObjectWithTag("StageArea").GetComponent<ItemSpawnManager>();
            textTimer = GameObject.Find("TextTimer").GetComponent<Text>();

            imageCountdown = GameObject.Find("ImageCountdown").GetComponent<Image>();

            generator.SetControlable(false);
            StartCoroutine(Countdown());

            imageTimerOver = GameObject.Find("ImageTimerOver").GetComponent<Image>();
            imageTimerOver.enabled = false;

            GameObject resultAlpha = GameObject.Find("BackgroundAlpha");

            imageCountdown = GameObject.Find("ImageCountdown").GetComponent<Image>();

            imagePlayer = GameObject.Find("ImagePlayer").GetComponent<Image>();

            for (int i = 0; i < 4; i++)
            {
                gauge[i] = new Gauge();

                gauge[i].obj = GameObject.Find("Gauge" + (i + 1));
                gauge[i].textPercent = gauge[i].obj.transform.Find("TextPercent").GetComponent<Text>();
            }

            result = GameObject.Find("Result");
            panelResult = GameObject.Find("PanelResult");

            for (int i = 0; i < 4; i++)
            {
                resultPlayer[i] = panelResult.transform.Find("Player" + (i + 1)).gameObject;
                resultPlayer[i].transform.SetParent(GameObject.Find("Result").transform.Find("Hide"));
            }

            panelResult.SetActive(false);
            result.SetActive(false);
        }
	}
	
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if(!isFinish)
            {
                CheckGauge();

                for (int i = 0; i < 4; i++)
                {
                    value[i] = TileChecker.tileCount[i+1];
                    gauge[i].value = value[i];
                    gauge[i].ChangeWidth();

                    if (i == 0)
                        gauge[i].obj.transform.position = new Vector2(0, 1050f);
                    else
                        gauge[i].obj.transform.position = new Vector2(gauge[i - 1].obj.transform.position.x + gauge[i - 1].obj.GetComponent<RectTransform>().rect.width, 1050f);
                }
            }
            else
            {
                if (!panelResult.activeSelf)
                {
                    soundManager.BgmResult();

                    result.SetActive(true);
                    panelResult.SetActive(true);

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
                                resultPlayer[j].transform.Find("Rank").transform.Find("Text").GetComponent<Text>().text = (count + 1).ToString();

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
                        resultPlayer[i].transform.Find("Gauge").transform.Find("TextPercent").GetComponent<Text>().text = Mathf.Round(gauge[i].percent).ToString() + "%";

                        gauge[i].obj = resultPlayer[i].transform.Find("Gauge").gameObject;
                        gauge[i].ResultWidth();
                    }
                }
            }
        }

        if(SceneManager.GetActiveScene().name == "Main")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ShowMain();
        }
    }

    public void ShowMain()
    {
        soundManager.BgmTitle();
        soundManager.PlaySound(4);

        select.SetActive(false);
        credit.SetActive(false);

        main.SetActive(true);
    }

    public void ShowSelect()
    {
        StartCoroutine(ChangeMenu(1));
        soundManager.PlaySound(4);

        main.SetActive(false);
        credit.SetActive(false);

        select.SetActive(true);
    }

    public void ShowCredit()
    {
        credit.SetActive(true);
        soundManager.PlaySound(4);

        soundManager.BgmCredit();

        main.SetActive(false);
        select.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameStart(int playerCount)
    {
        soundManager.PlaySound(4);

        if (menu == 1)
        {
            UIManager.playerCount = playerCount;
            Debug.Log("pc : " + playerCount);
            SceneManager.LoadScene("GameScene");
        }

        //SceneManager.LoadScene("GameScene");
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
        {
            soundManager.source[1].Stop();
            soundManager.PlaySound(9);

            StartCoroutine(TimeOver());
        }
        else
            StartCoroutine(Timer());
    }

    void CheckGauge()
    {
        gaugeAll = TileChecker.tileCount[1] + TileChecker.tileCount[2] + TileChecker.tileCount[3] + TileChecker.tileCount[4];
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

        obj.SetActive(check);
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
            generator.SetControlable(true);
            spawner.StartCoroutine(spawner.SpawnItemRandomly());
            GameObject.Find("ImageCountdown").SetActive(false);
            StartCoroutine(Timer());
        }
    }

    IEnumerator TimeOver()
    {
        imageTimerOver.enabled = true;
        generator.SetControlable(false);

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