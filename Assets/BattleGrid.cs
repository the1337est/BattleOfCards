using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour
{

    public GridArea SideA;
    public GridArea SideB;

    void Start ()
    {
        SideA.Init(1);
        SideB.Init(2);
	}
	
}
