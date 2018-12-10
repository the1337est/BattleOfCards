using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour
{

    public ChampionData ChampionData;

    public Text HealthText;
    public Text AttackText;
    public Image Icon;

    private void Awake()
    {
        Set(ChampionData);
    }

    public void Set(ChampionData champion)
    {
        HealthText.text = champion.MaxHealth.ToString();
        AttackText.text = champion.Damage.ToString();
        Icon.sprite = champion.Portrait;
    }
	
}
