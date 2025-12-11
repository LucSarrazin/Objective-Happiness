using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private RectTransform arrowRectTransform;
    [SerializeField] private Toggle x3;
    [SerializeField] private Toggle playPause;
    [SerializeField] private float dayStartRotation;
    [SerializeField] private float nightStartRotation;
    private float dayRotationRange;
    private float nightRotationRange;

    void Awake()
    {
        dayRotationRange = nightStartRotation - dayStartRotation;
        nightRotationRange = 360f - dayRotationRange;
    }

    void Update()
    {
        if (GameManager.Instance.night)
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, nightStartRotation + (GameManager.Instance.elapsedTime - GameManager.Instance.dayDuration) / GameManager.Instance.nightDuration * nightRotationRange);
        else
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, dayStartRotation + GameManager.Instance.elapsedTime / GameManager.Instance.dayDuration * dayRotationRange);
    }

    public void ChangeSpeed()
    {
        if (x3.isOn)
            GameManager.Instance.SetTimeSpeed(3f);
        else
            GameManager.Instance.SetTimeSpeed(1f);
    }

    public void Pause()
    {
        if (playPause.isOn)
            GameManager.Instance.StopTime();
        else
            GameManager.Instance.StartTime();
    }
}
