using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public GameObject Fire;

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
