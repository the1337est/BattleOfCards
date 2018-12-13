using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public GameObject Fire;
    public Player Player;

    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            hp = hp < 0 ? 0 : hp;
            UIManager.Instance.UpdateHP();
            if (hp == 0)
            {
                SetFire(true);
                GameManager.Instance.GameOver(Player);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    public bool Enabled
    {
        get
        {
            return Fire.activeSelf;
        }
    }

    public void SetFire(bool enable)
    {
        Fire.SetActive(enable);
    }

}
