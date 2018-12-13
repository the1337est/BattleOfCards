using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler
{

    public Image Image;
    DeckSlot slot;

    void Start()
    {
        Image = GetComponent<Image>();
        slot = GetComponent<DeckSlot>();

    }


    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slot.Allow)
        {
            GameManager.Instance.TouchDown(slot.ChampionData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
