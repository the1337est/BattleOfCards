using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{

    public const string CHAMPIONS_PATH = "Champions/";
    public Champion ChampionPrefab;

    public Champion B1, B2, B3, R1, R2, R3;

    public HealthBar HealthBarPrefab;

    public Canvas WorldSpace;

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
    public float AttackDuration = 0.3f;

    public Material BlueMaterial;
    public Material RedMaterial;

    public Color RedHighlightColor;
    public Color BlueHighlightColor;
    public Color RedSlotColor;
    public Color BlueSlotColor;

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

    private GridSlot highlightedSlot;

    private void CheckTurn()
    {
        if (Turn == Player.Blue)
        {
            if (Data.BlueMana < UIManager.Instance.Deck.MinimumCost)
            {
                ChangeTurn();
            }
        }
        else
        {
            if (Data.RedMana < UIManager.Instance.Deck.MinimumCost)
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
        //if (side == Player.Blue)
        //{
        //    StartCoroutine(grid.TeamB.Attack());
        //}
        //else
        //{
        //    if (OnTurnComplete != null)
        //    {
        State = GameState.Playing;
        if (side == Player.Blue)
        {
            Turn = Player.Red;
        }
        else
        {
            Turn = Player.Blue;
        }
        OnTurnComplete();
            //}
        //}
    }

    public void StartGame()
    {
        State = GameState.Playing;

        
        Turn = Random.value > 0.5 ? Player.Blue : Player.Red;
        UIManager.Instance.Deck.LoadDeck(Turn);
        BlueCastle.HP = CastleHPAtStart;
        RedCastle.HP = CastleHPAtStart;
        Data = new MatchData(CastleHPAtStart, ManaAtStart, CastleHPAtStart, ManaAtStart);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.QuitGame();
        }

        if (State == GameState.Playing)
        {
            if (Dragging)
            {
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit dragHit;
                bool onSlot = false;
                bool canPlace = false;

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
                                    canPlace = true;
                                    onSlot = true;
                                    currentSlot = slot;
                                    currentSlot.Highlight(true);
                                    highlightedSlot = currentSlot;
                                    Target.transform.position = new Vector3(slot.transform.position.x, 0.25f, slot.transform.position.z);
                                }
                            }
                        }
                        else
                        {
                            Vector3 pos = dragHit.point;
                            Target.transform.position = new Vector3(pos.x, 0.25f, pos.z);
                        }
                    }
                }

                Target.transform.localScale = canPlace ? Vector3.one * 0.5f : Vector3.one * 0.3f;

                if (canPlace)
                {
                    if (highlightedSlot != null)
                    {
                        highlightedSlot.Highlight(true);
                    }

                }
                else
                {
                    if (highlightedSlot != null)
                    {
                        highlightedSlot.Highlight(false);
                    }
                    highlightedSlot = null;
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
                            
                            Target.transform.parent = currentSlot.transform;
                            Target.transform.localPosition = Vector3.up * 0.25f;
                            Target.Slot = currentSlot;
                            currentSlot.Champion = Target;
                            currentSlot.Highlight(false);
                            HealthBar bar = Instantiate(HealthBarPrefab, WorldSpace.transform);
                            Target.Bar = bar;
                            Target.Spawn();
                            currentDeckSlot.LoadNewCard(Turn);
                            placed = true;

                            if (Turn == Player.Blue)
                            {
                                Data = Data.Add(Player.Blue, StatType.Mana, -currentChampion.Cost);
                            }
                            else
                            {
                                Data = Data.Add(Player.Red, StatType.Mana, -currentChampion.Cost);
                            }
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
            State = GameState.Animating;
            Data = Data.Add(Player.Blue, StatType.Mana, ManaPerRound);
            Simulate();
            Turn = Player.Red;
        }
        else
        {
            
            State = GameState.Animating;
            Data = Data.Add(Player.Red, StatType.Mana, ManaPerRound);
            Simulate();
            Turn = Player.Blue;
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

            Champion champ = null;
            switch (champion.Name)
            {
                case "Archer":
                    champ = B1;
                    break;
                case "Barbarian":
                    champ = B2;
                    break;
                case "Knight":
                    champ = B3;
                    break;
                case "Minion":
                    champ = R1;
                    break;
                case "Dragon":
                    champ = R2;
                    break;
                case "Troll":
                    champ = R3;
                    break;
            }

            Champion c = Instantiate(champ, transform);
            c.transform.localScale = Vector3.one * 0.3f;
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
        if (Turn == Player.Blue)
        {
            StartCoroutine(grid.TeamA.Attack());
        }
        else
        {
            StartCoroutine(grid.TeamB.Attack());
        }
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
