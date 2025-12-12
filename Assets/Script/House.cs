using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class House : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool sleeping;

    void Update()
    {
        if (sleeping)
            animator.Play("house wiggle");
        else
            animator.Play("idle");
    }
}
