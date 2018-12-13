using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{

    public RectTransform MenuUI;
    public RectTransform GameUI;
    public RectTransform CardDeck;
    public RectTransform GameOver;

    public Text BlueHP;
    public Text BlueMana;
    public Text RedHP;
    public Text RedMana;
    public Text Turn;
    public Text Win;
    public Text Timer;

    int timer = 0;

    public Image DeckImage;

    public Color BlueColor;
    public Color RedColor;

    public CardDeck Deck;

    public Button PassButton;

    public void StartGame()
    {
        MenuUI.gameObject.SetActive(false);
        GameUI.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
        InvokeRepeating("Tick", 1f, 1f);
    }



    private void OnEnable()
    {
        GameManager.OnMatchDataChanged += UpdateMatchUI;
        GameManager.OnTurnComplete += OnTurnComplete;
    }

    private void OnTurnComplete()
    {
        UpdateTurn(Player.Blue);
    }

    private void OnDisable()
    {
        GameManager.OnMatchDataChanged -= UpdateMatchUI;
        GameManager.OnTurnComplete -= OnTurnComplete;
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
        CardDeck.gameObject.SetActive(GameManager.Instance.State == GameState.Playing);
        PassButton.gameObject.SetActive(GameManager.Instance.State != GameState.Animating);
    }

    public void UpdateMatchUI(MatchData data)
    {

        //Debug.Log("Data: " + data.BlueMana + " | " + data.RedMana);
        BlueMana.text = data.BlueMana.ToString();
        RedMana.text = data.RedMana.ToString();
    }

    public void UpdateHP()
    {
        BlueHP.text = GameManager.Instance.BlueCastle.HP.ToString();
        RedHP.text = GameManager.Instance.RedCastle.HP.ToString();
    }

    public void ShowGameOver(Player winner)
    {
        Win.text = winner.ToString().ToUpper() + " WINS!";
        GameUI.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(true);

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Game");
            
    }

    private void Tick()
    {
        if (GameManager.Instance.State != GameState.Menu)
        {
            timer++;
            TimeSpan span = TimeSpan.FromSeconds(timer);
            Timer.text = string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
        }
    }

}
