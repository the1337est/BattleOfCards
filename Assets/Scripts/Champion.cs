using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Champion : MonoBehaviour
{

    public ChampionData Data;

    private int direction = 1;

    public int Health
    {
        get;
        set;
    }

    public GridSlot Slot;
    public ChampionState State;

    public void Initialize(ChampionData data)
    {
        Data = data;
        Health = Data.MaxHealth;
        State = ChampionState.None;
    }

    public void Spawn(Vector2 position)
    {
        State = ChampionState.Idle;
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
