using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BattleGrid : MonoBehaviour
{

    public Team TeamA;
    public Team TeamB;

    public bool Refresh = false;

    void Update()
    {
        if (Refresh)
        {
            Refresh = false;
            TeamA.Unload();
            TeamB.Unload();
            TeamA.Init(Player.Blue);
            TeamB.Init(Player.Red);
        }
    }

    public GridRow GetOpponentRow(Player player, int rowID)
    {
        if (player == Player.Blue)
        {
            return TeamB.Rows[rowID];
        }
        else
        {
            return TeamA.Rows[rowID];
        }
    }

    public Castle GetOpponentCastle(Player player)
    {
        if (player == Player.Blue)
        {
            return GameManager.Instance.RedCastle;
        }
        else
        {
            return GameManager.Instance.BlueCastle;
        }
    }
}
