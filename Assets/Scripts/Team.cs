using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{

    public Player Side { get; private set; }

    public List<GridRow> Rows;

    public GridRow RowPrefab;

    public List<Champion> Champions;

    public List<GameObject> ChampionPrefabs;

    public Player PlayerType;

    public int Energy;

    public void Add(Champion champion)
    {
        Champions.Add(champion);
    }

    public void Remove(Champion champion)
    {
        Champions.Remove(champion);
    }

    [ExecuteInEditMode]
    public void Init(Player side)
    {

        Side = side;

        for (int i = 0; i < 4; i++)
        {
            GridRow row = Instantiate(RowPrefab, transform);
            row.InitializeSlots(Side, i);
            Rows.Add(row);
            row.name = "Row " + i;
        }
    }

    [ExecuteInEditMode]
    public void Unload()
    {

        List<Transform> toRemove = new List<Transform>();
        foreach (Transform t in GetComponentInChildren<Transform>(true))
        {
            toRemove.Add(t);
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            DestroyImmediate(toRemove[i].gameObject);
        }
        Rows.Clear();
    }   

    public void AutoPlay()
    {

    }

}
