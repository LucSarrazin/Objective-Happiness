using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlip : MonoBehaviour
{
    [SerializeField] private bool flipOnStart = true;
    [SerializeField] private bool modifySize = false;

    void Start()
    {
        if (modifySize)
            transform.localScale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f));


        if (flipOnStart && Random.value > 0.5f)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y * 1, transform.localScale.z * 1);
    }
}
