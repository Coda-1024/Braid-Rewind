using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float maxH = 8;
   
    public void ShadowMove(Vector3 pos)
    {
        transform.position = pos;
    }

    public void ShadowScale(float h)
    {
        transform.localScale = (1 - h / maxH) * new Vector3(1.5f, 1.5f, 1.5f);
    }

}
