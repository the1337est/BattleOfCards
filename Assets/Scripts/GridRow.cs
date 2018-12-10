using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRow : MonoBehaviour
{

    public int Side;
    public int RowID;

    public List<GridSlot> Slots;
    public GridSlot SlotPrefab;

    [ExecuteInEditMode]
    public void InitializeSlots(int side, int rowId)
    {

        RowID = rowId;
        Side = side;

        for (int i = 0; i < 3; i++)
        {
            GridSlot slot = Instantiate(SlotPrefab, transform);
            slot.Init(this, new GridPos { Side = Side, X = i, Y = RowID });
            Slots.Add(slot);
        }
    }
}
