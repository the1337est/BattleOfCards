using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRow : MonoBehaviour
{

    public Player Side;
    public int RowID;

    public List<GridSlot> Slots;
    public GridSlot SlotPrefab;

    [ExecuteInEditMode]
    public void InitializeSlots(Player side, int rowId)
    {

        RowID = rowId;
        Side = side;

        for (int i = 0; i < 3; i++)
        {
            GridSlot slot = Instantiate(SlotPrefab, transform);
            slot.Init(this, new GridPos { Side = side, X = i, Y = RowID });
            Slots.Add(slot);
        }
    }

    public GridSlot GetFirstTarget(Player player)
    {
        if (player == Player.Red)
        {
            for (int i = Slots.Count - 1; i >= 0; i--)
            {
                if (!Slots[i].IsEmpty)
                {
                    return Slots[i];
                }
            }
            return null;
        }
        else
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (!Slots[i].IsEmpty)
                {
                    return Slots[i];
                }
            }
            return null;
        }

    }

}
