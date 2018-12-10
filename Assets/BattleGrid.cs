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
            TeamA.Init(1);
            TeamB.Init(2);
        }
    }

}
