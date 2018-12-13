using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{

    public List<DeckSlot> Slots;

    public int MinimumCost
    {
        get
        {
            int min = int.MaxValue;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].ChampionData.Cost < min)
                {
                    min = Slots[i].ChampionData.Cost;
                }
            }
            return min;
        }
    }

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

    public ChampionData GetRandomChampion(Player player)
    {

        if (player == Player.Blue)
        {
            return GameManager.Instance.BlueCards[Random.Range(0, GameManager.Instance.BlueCards.Count)];
        }
        else
        {
            return GameManager.Instance.RedCards[Random.Range(0, GameManager.Instance.RedCards.Count)];
        }
    }
	
}
