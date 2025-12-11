using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlip : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;

    void Start()
    {
        if (Random.value > 0.5f)
            for (int i = 0; i < spriteRenderers.Count; i++)
                spriteRenderers[i].flipX = !spriteRenderers[i].flipX;
    }
}
