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

    private void Awake()
    {
        Set(ChampionData);
    }

    public void Set(ChampionData champion)
    {
        HealthText.text = champion.MaxHealth.ToString();
        AttackText.text = champion.Damage.ToString();
        ManaText.text = champion.Cost.ToString();
        Icon.sprite = champion.Portrait;
        BG.color = champion.Background;
    }
	
}
