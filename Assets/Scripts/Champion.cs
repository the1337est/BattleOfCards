using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Champion : MonoBehaviour
{

    public ChampionData Data;
    public Text HPText;

    private int direction = 1;

    int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            //HPText.text = health.ToString();
        }
    }


    public GridSlot Slot;
    public ChampionState State;

    public void Spawn()
    {
        Health = Data.MaxHealth;
    }

    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    private void Update()
    {
        //if (direction == 1)
        //{
        //    if (0.6f - transform.position.y <= 0.05f)
        //    {
        //        direction = -1;
        //    }
        //    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0.6f, transform.position.z), Time.deltaTime * 1.2f);

        //}
        //else
        //{
        //    if (transform.position.y - 0.3f <= 0.05f)
        //    {
        //        direction = 1;
        //    }
        //    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0.3f, transform.position.z), Time.deltaTime * 1.2f);
        //}
        
    }

}
