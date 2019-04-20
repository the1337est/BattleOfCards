using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Champion : MonoBehaviour
{

    public ChampionData Data;

    public HealthBar Bar;

    private int direction = 1;
    private float scale = 0.01f;

    int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            Bar.Set(Mathf.Clamp01((float)health / (float)Data.MaxHealth));
            //HPText.text = health.ToString();
        }
    }
    bool logged = false;
        
    public GridSlot Slot;
    public ChampionState State;

    public void Spawn()
    {
        Health = Data.MaxHealth;
    }

    public void SetColor(Color color)
    {
        //GetComponent<MeshRenderer>().material.color = color;
    }

    private void Update()
    {

        if (Bar != null)
        {
            if (!logged)
            {
                logged = true;
                Debug.Log("Champion pos: " + transform.position + " | Parameter: " + (transform.position + (Vector3.up * 0.35f)) + " | Final: " + Camera.main.WorldToScreenPoint(transform.position + (Vector3.up * 0.35f)));
            }
            Bar.transform.position = (transform.position + (Vector3.up * 0.45f));
        }

    }


    private void OnDestroy()
    {
        if (Bar != null)
        {
            Destroy(Bar.gameObject);
        }
    }
}
