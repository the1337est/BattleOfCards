﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChampionData : ScriptableObject
{

    public string Name;
    public int MaxHealth;
    public int Damage;
    public ChampionType Type;

    public GameObject Model;
    public Sprite Portrait;

}
