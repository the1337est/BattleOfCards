using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public const string CHAMPIONS_PATH = "Champions/";
    public Champion ChampionPrefab;

    public GridRow RowPrefab;
    public GridSlot SlotPrefab;

    public bool Dragging = false;
    public Champion Target = null;

    GridSlot currentSlot;
    Camera cam;

    public GameState State;

    private void Awake()
    {
        cam = Camera.main;
        State = GameState.Menu;
    }

    public delegate void TurnDelegate();
    public static event TurnDelegate OnTurnComplete;

    public void StartGame()
    {
        State = GameState.PlayerTurn;
    }

    private void Update()
    {
        if (State == GameState.PlayerTurn)
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
                            if (slot != null && slot.ParentRow.Side == 1)
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
                        Debug.Log("Clicked on slot: Side: " + currentSlot.Position.Side + " | X: " + currentSlot.Position.X + " | Y: " + currentSlot.Position.Y);
                        Target.transform.parent = currentSlot.transform;
                        Target.transform.localPosition = Vector3.up * 0.25f;
                        Target.Slot = currentSlot;
                        currentSlot.Champion = Target;
                        placed = true;
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
                }
            }
        }
    }

    public void TouchDown(Color color)
    {
        if (!Dragging)
        {
            Dragging = true;
            Champion c = Instantiate(ChampionPrefab, transform);
            c.SetColor(color);
            Target = c;
        }
    }

    public void SpawnAtSlot(GridSlot slot, Champion champion)
    {
        Champion c = Instantiate(champion, slot.transform);
        c.Slot = slot;
        slot.Champion = c;
    }

    public void EnemyTurn()
    {

    }

}
