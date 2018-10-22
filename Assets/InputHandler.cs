using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler
{

    public Image Image;

    void Start()
    {
        Image = GetComponent<Image>();

    }


    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.TouchDown(Image.color);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
