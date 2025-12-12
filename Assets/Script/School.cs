using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class School : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool maxStudent;

    void Update()
    {
        if (maxStudent)
            animator.Play("house wiggle");
        else
            animator.Play("idle");
    }
}
