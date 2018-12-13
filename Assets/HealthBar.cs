using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image Fill;

    public void Set(float val)
    {
        Fill.rectTransform.localScale = new Vector3(val, 1f, 1f);
        Fill.color = Color.Lerp(Color.red, Color.green, val);
    }

}
