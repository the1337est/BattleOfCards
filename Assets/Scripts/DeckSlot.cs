using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour
{

    public ChampionData ChampionData;

    public Text HealthText;
    public Text AttackText;
    public Text ManaText;
    public Image Icon;
    public Image BG;

    public Image Blocker;

    public bool Allow { get { return !Blocker.gameObject.activeSelf; } }

    private CardDeck deck;

    private void Awake()
    {
        deck = FindObjectOfType<CardDeck>();
    }

    public void Set(ChampionData champion)
    {
        ChampionData = champion;
        HealthText.text = champion.MaxHealth.ToString();
        AttackText.text = champion.Damage.ToString();
        ManaText.text = champion.Cost.ToString();
        Icon.sprite = champion.Portrait;
        BG.color = champion.Background;
    }

    private void OnEnable()
    {
        GameManager.OnMatchDataChanged += OnDataUpdate;
    }

    private void OnDataUpdate(MatchData data)
    {
        if (GameManager.Instance.Turn == Player.Blue)
        {
            Blocker.gameObject.SetActive(data.BlueMana < ChampionData.Cost);
        }
        else
        {
            Blocker.gameObject.SetActive(data.RedMana < ChampionData.Cost);
        }
    }

    public void LoadNewCard(Player player)
    {
        Set(deck.GetRandomChampion(player));
    }

}
