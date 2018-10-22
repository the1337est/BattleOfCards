using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public const string CHAMPIONS_PATH = "Champions/";
    public Champion ChampionPrefab;

    public GridRow RowPrefab;
    public GridSlot SlotPrefab;


    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject != null)
                {
                    GridSlot slot = hit.collider.GetComponent<GridSlot>();
                    if (slot != null)
                    {
                        if (slot.IsEmpty)
                        {
                            Debug.Log("Clicked on slot: Side: " + slot.Position.Side + " | X: " + slot.Position.X + " | Y: " + slot.Position.Y);
                            Champion c = Instantiate(ChampionPrefab, slot.transform);
                            c.transform.localPosition = Vector3.up * 0.25f;
                            slot.Champion = c;
                        }
                    }
                }
            }
        }
    }

}
