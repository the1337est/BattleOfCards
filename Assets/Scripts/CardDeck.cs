﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{

    public List<DeckSlot> Slots;

    public void LoadDeck(Player player)
    {
        if (player == Player.Blue)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                ChampionData data = GameManager.Instance.BlueCards[Random.Range(0, GameManager.Instance.BlueCards.Count)];
                Slots[i].Set(data);
            }
        }
        else
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                ChampionData data = GameManager.Instance.RedCards[Random.Range(0, GameManager.Instance.RedCards.Count)];
                Slots[i].Set(data);
            }
        }
    }
	
}
