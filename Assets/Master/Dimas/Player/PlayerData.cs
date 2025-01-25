using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int point;

    public float DefaultSpeed;
    public float RuntimeSpeed;

    public bool isSpeedEffect;
    public bool isStunEffect;
    public bool isStreakEffect;

    public Coroutine SpeedCorotine, StreakCoroutine;

    public PlayerData()
    {
        RuntimeSpeed = DefaultSpeed;
    }
    public IEnumerator ChangeSpeed(float time, float value, Action onFinished)
    {
        Debug.Log("Change Speed");

        RuntimeSpeed = value;

        yield return new WaitForSeconds(time);

        Debug.Log("Default Change Speed");

        RuntimeSpeed = DefaultSpeed;
        onFinished?.Invoke();
    }
    public IEnumerator GetStreakEffect(float time)
    {
        isStreakEffect = true;

        yield return new WaitForSeconds(time);

        isStreakEffect = false;
    }
}
