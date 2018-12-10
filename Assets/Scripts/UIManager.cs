using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public RectTransform MenuUI;
    public RectTransform GameUI;

    public Text PlayerHP;
    public Text PlayerMana;
    public Text EnemyHP;
    public Text EnemyMana;

    public void StartGame()
    {
        MenuUI.gameObject.SetActive(false);
        GameUI.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
    }

    private void OnEnable()
    {
        GameManager.OnMatchDataChanged += UpdateMatchUI;
    }

    private void OnDisable()
    {
        GameManager.OnMatchDataChanged -= UpdateMatchUI;
    }

    public void UpdateMatchUI(MatchData data)
    {
        PlayerHP.text = data.PHP.ToString();
        EnemyHP.text = data.EHP.ToString();
        PlayerMana.text = data.PMana.ToString();
        EnemyMana.text = data.EMana.ToString();

    }

}
