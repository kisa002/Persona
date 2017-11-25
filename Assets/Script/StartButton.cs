using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(223f / 255f, 100f / 255f, 20f / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(176f / 255f, 75f / 255f, 9f / 255f);
    }    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameObject.Find("UIManager").GetComponent<UIManager>().menu == 1)
            SceneManager.LoadScene("Game");
    }
}