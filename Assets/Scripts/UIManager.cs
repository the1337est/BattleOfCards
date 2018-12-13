using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public RectTransform MenuUI;
    public RectTransform GameUI;

    public Text BlueHP;
    public Text BlueMana;
    public Text RedHP;
    public Text RedMana;
    public Text Turn;

    public Image DeckImage;

    public Color BlueColor;
    public Color RedColor;

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

    public void UpdateTurn(Player player)
    {
        Turn.text = player.ToString() + "'s Turn";
        switch (player)
        {
            case Player.Blue:
                DeckImage.color = BlueColor;
                break;
            case Player.Red:
                DeckImage.color = RedColor;
                break;
            default:
                break;
        }
    }

    public void UpdateMatchUI(MatchData data)
    {
        BlueHP.text = data.BlueHP.ToString();
        RedHP.text = data.RedHP.ToString();
        BlueMana.text = data.BlueHP.ToString();
        RedMana.text = data.RedHP.ToString();

    }

}
