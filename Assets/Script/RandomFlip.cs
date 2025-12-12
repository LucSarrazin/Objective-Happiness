using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlip : MonoBehaviour
{
    void Start()
    {
        if (Random.value > 0.5f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
