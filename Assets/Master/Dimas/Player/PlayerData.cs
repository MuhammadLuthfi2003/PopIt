using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int point;

    public float Speed;

    public bool isSpeedEffect;
    public bool isStunEffect;
    public bool isStreakEffect;

    public IEnumerator ChangeSpeed(float time, float value, Action onFinished)
    {
        Debug.Log("Change Speed");

        float defaultSpeed = Speed;

        Speed = value;

        yield return new WaitForSeconds(time);

        Debug.Log("Default Change Speed");

        Speed = defaultSpeed;
        onFinished?.Invoke();
    }
    public IEnumerator GetStreakEffect(float time)
    {
        isStreakEffect = true;

        yield return new WaitForSeconds(time);

        isStreakEffect = false;
    }
}
