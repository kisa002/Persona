using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    UIManager uiManager;

    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (this.gameObject.name)
        {
            case "Start2":
                this.gameObject.GetComponent<Image>().enabled = true;
                GameObject.Find("Start3").GetComponent<Image>().enabled = false;
                GameObject.Find("Start4").GetComponent<Image>().enabled = false;

                this.gameObject.transform.FindChild("Text").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("Start3").transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                GameObject.Find("Start4").transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                break;

            case "Start3":
                this.gameObject.GetComponent<Image>().enabled = true;
                GameObject.Find("Start2").GetComponent<Image>().enabled = false;
                GameObject.Find("Start4").GetComponent<Image>().enabled = false;

                this.gameObject.transform.FindChild("Text").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("Start2").transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                GameObject.Find("Start4").transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                break;

            case "Start4":
                this.gameObject.GetComponent<Image>().enabled = true;
                GameObject.Find("Start2").GetComponent<Image>().enabled = false;
                GameObject.Find("Start3").GetComponent<Image>().enabled = false;

                this.gameObject.transform.FindChild("Text").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("Start2").transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                GameObject.Find("Start3").transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(this.gameObject.GetComponent<Image>().color.r, this.gameObject.GetComponent<Image>().color.g, this.gameObject.GetComponent<Image>().color.b, 0.8f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(this.gameObject.GetComponent<Image>().color.r, this.gameObject.GetComponent<Image>().color.g, this.gameObject.GetComponent<Image>().color.b, 0.5f);
        this.gameObject.GetComponent<Image>().enabled = false;

        this.gameObject.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Game");
    }
}
