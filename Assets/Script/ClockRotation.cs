using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotation : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private float startRotation;
    [SerializeField] private float endRotation;
    private float dayRotationRange;
    private float nightRotationRange;

    void Awake()
    {
        dayRotationRange = endRotation - startRotation;
        nightRotationRange = 360f - dayRotationRange;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (GameManager.Instance.night)
            rectTransform.rotation = Quaternion.Euler(0, 0, endRotation + (GameManager.Instance.elapsedTime - GameManager.Instance.dayDuration) / GameManager.Instance.nightDuration * nightRotationRange);
        else
            rectTransform.rotation = Quaternion.Euler(0, 0, startRotation + GameManager.Instance.elapsedTime / GameManager.Instance.dayDuration * dayRotationRange);
    }
}
