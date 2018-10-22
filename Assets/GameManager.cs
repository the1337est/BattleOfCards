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

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Dragging)
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit dragHit;

            if (Physics.Raycast(r, out dragHit, 20))
            {
                if (dragHit.collider != null)
                {
                    GridSlot slot = dragHit.collider.GetComponent<GridSlot>();
                    if (slot != null)
                    {
                        if (slot.IsEmpty)
                        {
                            Target.transform.position = new Vector3(slot.transform.position.x, 0.25f, slot.transform.position.z);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider != null)
                    {
                        GridSlot slot = hit.collider.GetComponent<GridSlot>();
                        if (slot != null)
                        {
                            if (slot.IsEmpty)
                            {
                                Debug.Log("Clicked on slot: Side: " + slot.Position.Side + " | X: " + slot.Position.X + " | Y: " + slot.Position.Y);
                                Target.transform.parent = slot.transform;
                                Target.transform.localPosition = Vector3.up * 0.25f;
                                slot.Champion = Target;

                            }
                        }
                    }
                }
                Dragging = false;
                Target = null;
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

}
