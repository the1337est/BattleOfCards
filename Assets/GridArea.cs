using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridArea : MonoBehaviour
{

    public int AreaID { get; private set; }

    public List<GridRow> Rows;

    public void Init(int areaID)
    {
        if (areaID >= 1 && areaID <= 2)
        {
            AreaID = areaID;

            for (int i = 0; i < 4; i++)
            {
                GridRow row = Instantiate(GameManager.Instance.RowPrefab, transform);
                row.InitializeSlots(AreaID, i);
                Rows.Add(row);
                row.name = "Row " + i;
            }
        }
    }

}
