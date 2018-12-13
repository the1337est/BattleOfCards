using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MatchData
{

    public int BlueHP;
    public int BlueMana;
    public int RedHP;
    public int RedMana;

    public MatchData(int blueHP, int blueMana, int redHP, int redMana)
    {
        BlueHP = blueHP;
        BlueMana = blueMana;
        RedHP = redHP;
        RedMana = redMana;
    }

    public MatchData Add(Player player, StatType type, int val)
    {
        MatchData matchData = new MatchData(BlueHP, BlueMana, RedHP, RedMana);
        if (player == Player.Blue)
        {
            if (type == StatType.HP)
            {
                matchData.BlueHP += val;
            }
            else
            {
                matchData.BlueMana += val;
            }
        }
        else
        {
            if (type == StatType.HP)
            {
                matchData.RedHP += val;
            }
            else
            {
                matchData.RedMana += val;
            }
        }
        return matchData;
    }

}

public enum StatType
{
    HP,
    Mana
}