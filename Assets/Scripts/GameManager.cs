using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public const string CHAMPIONS_PATH = "Champions/";
    public Champion ChampionPrefab;

    public GridRow RowPrefab;
    public GridSlot SlotPrefab;

    public Castle BlueCastle;
    public Castle RedCastle;

    private BattleGrid grid;

    public bool Dragging = false;
    public Champion Target = null;

    GridSlot currentSlot;
    Camera cam;

    public List<ChampionData> BlueCards;
    public List<ChampionData> RedCards;

    private ChampionData currentChampion = null;
    private DeckSlot currentDeckSlot = null;

    public delegate void TurnDelegate();
    public static event TurnDelegate OnTurnComplete;

    public delegate void MatchDataDelegate(MatchData data);
    public static event MatchDataDelegate OnMatchDataChanged;

    public int ManaAtStart = 5; 
    public int ManaPerRound = 5;
    public int CastleHPAtStart = 25;


    public GameState State;

    private Player turn;
    public Player Turn
    {
        get
        {
            return turn;
        }
        set
        {
            turn = value;
            UIManager.Instance.UpdateTurn(Turn);
        }
    }

    private MatchData data;
    public MatchData Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
            CheckTurn();
            if (OnMatchDataChanged != null)
            {
                //Debug.Log("Data was changed");
                OnMatchDataChanged(data);
            }
            
        }

    }

    private void CheckTurn()
    {
        if (Turn == Player.Blue)
        {
            if (Data.BlueMana < 1)
            {
                ChangeTurn();
            }
        }
        else
        {
            if (Data.RedMana < 1)
            {
                ChangeTurn();
            }
        }
    }

    private void Awake()
    {
        cam = Camera.main;
        State = GameState.Menu;
        grid = FindObjectOfType<BattleGrid>();
    }

    private void OnEnable()
    {
        Team.OnAttackFinish += OnTeamAttackFinish;

    }

    private void OnDisable()
    {
        Team.OnAttackFinish -= OnTeamAttackFinish;
    }

    private void OnTeamAttackFinish(Player side)
    {

        Debug.Log("Team " + side.ToString() + " finished attack");
        if (side == Player.Blue)
        {
            StartCoroutine(grid.TeamB.Attack());
        }
        else
        {
            if (OnTurnComplete != null)
            {
                State = GameState.Playing;
                Turn = Player.Blue;
                OnTurnComplete();
            }
        }
    }

    public void StartGame()
    {
        State = GameState.Playing;
        Turn = Player.Blue;
        UIManager.Instance.Deck.LoadDeck(Turn);
        BlueCastle.HP = CastleHPAtStart;
        RedCastle.HP = CastleHPAtStart;
        Data = new MatchData(CastleHPAtStart, ManaAtStart, CastleHPAtStart, ManaAtStart);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BlueCastle.SetFire(!BlueCastle.Enabled);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RedCastle.SetFire(!RedCastle.Enabled);
        }

        if (State == GameState.Playing)
        {
            if (Dragging)
            {
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit dragHit;
                bool onSlot = false;

                if (Physics.Raycast(r, out dragHit, 20))
                {
                    if (dragHit.collider != null)
                    {
                        if (dragHit.collider.tag == "Slot")
                        {
                            GridSlot slot = dragHit.collider.GetComponent<GridSlot>();
                            if (slot != null && slot.ParentRow.Side == Turn)
                            {
                                if (slot.IsEmpty)
                                {
                                    onSlot = true;
                                    currentSlot = slot;
                                    Target.transform.position = new Vector3(slot.transform.position.x, 0.25f, slot.transform.position.z);
                                }
                            }
                        }
                    }
                }

                if (!onSlot)
                {
                    currentSlot = null;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    bool placed = false;

                    if (currentSlot != null)
                    {

                        if ((Turn == Player.Blue && Data.BlueMana >= currentChampion.Cost) || (Turn == Player.Red && Data.RedMana >= currentChampion.Cost))
                        {
                            if (Turn == Player.Blue)
                            {
                                Data = Data.Add(Player.Blue, StatType.Mana, -currentChampion.Cost);
                            }
                            else
                            {
                                Data = Data.Add(Player.Red, StatType.Mana, -currentChampion.Cost);
                            }
                            
                            //Debug.Log("Clicked on slot: Side: " + currentSlot.Position.Side + " | X: " + currentSlot.Position.X + " | Y: " + currentSlot.Position.Y);
                            Target.transform.parent = currentSlot.transform;
                            Target.transform.localPosition = Vector3.up * 0.25f;
                            Target.Slot = currentSlot;
                            currentSlot.Champion = Target;
                            Target.Spawn();
                            currentDeckSlot.LoadNewCard(Turn);
                            placed = true;
                        }
                        
                    }

                    //if (Physics.Raycast(ray, out hit, 100))
                    //{
                    //    if (hit.collider != null)
                    //    {
                    //        GridSlot slot = hit.collider.GetComponent<GridSlot>();
                    //        if (slot != null)
                    //        {
                    //            if (slot.IsEmpty)
                    //            {
                    //                Debug.Log("Clicked on slot: Side: " + slot.Position.Side + " | X: " + slot.Position.X + " | Y: " + slot.Position.Y);
                    //                Target.transform.parent = slot.transform;
                    //                Target.transform.localPosition = Vector3.up * 0.25f;
                    //                slot.Champion = Target;
                    //                placed = true;
                    //            }
                    //        }
                    //    }
                    //}
                    if (!placed)
                    {
                        Destroy(Target.gameObject);
                    }
                    Dragging = false;
                    currentChampion = null;
                    currentDeckSlot = null;
                }
            }
        }
    }

    public void ChangeTurn()
    {
        if (Turn == Player.Blue)
        {
            Turn = Player.Red;
            Data = Data.Add(Player.Blue, StatType.Mana, ManaPerRound);
        }
        else
        {
            
            State = GameState.Animating;
            Turn = Player.Blue;
            Data = Data.Add(Player.Red, StatType.Mana, ManaPerRound);
            Simulate();
        }
        UIManager.Instance.Deck.LoadDeck(Turn);
    }

    public void TouchDown(ChampionData champion, DeckSlot slot)
    {
        if (!Dragging)
        {
            currentChampion = champion;
            currentDeckSlot = slot;
            Dragging = true;
            Champion c = Instantiate(ChampionPrefab, transform);
            c.Data = currentChampion;
            c.SetColor(champion.Background);
            Target = c;
        }
    }

    public void SpawnAtSlot(GridSlot slot, Champion champion)
    {
        Champion c = Instantiate(champion, slot.transform);
        c.Slot = slot;
        slot.Champion = c;
    }

    public void Simulate()
    {
        StartCoroutine(grid.TeamA.Attack());
    }

    public void GameOver(Player player)
    {
        if (player == Player.Blue)
        {
            UIManager.Instance.ShowGameOver(Player.Red);
        }
        else
        {
            UIManager.Instance.ShowGameOver(Player.Blue);
        }
    }

}
