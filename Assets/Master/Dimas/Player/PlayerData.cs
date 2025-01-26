using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int point;
    public bool isP1;

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

        if (isStunEffect)
        {
            if (isP1)
            {
                ShowPlayerEffect.instance.ToggleP2Stun(false);
            }
            else
            {
                ShowPlayerEffect.instance.ToggleP1Stun(false);
            }
        }
        else if (isSpeedEffect)
        {
            if (isP1)
            {
                ShowPlayerEffect.instance.ToggleP1Speed(false);
            }
            else
            {
                ShowPlayerEffect.instance.ToggleP2Speed(false);
            }
        }

            Debug.Log("Default Change Speed");

        RuntimeSpeed = DefaultSpeed;
        onFinished?.Invoke();
    }
    public IEnumerator GetStreakEffect(float time)
    {
        isStreakEffect = true;


        if (isP1)
        {
            ShowPlayerEffect.instance.ToggleP1Mult(true);
        }
        else
        {
            ShowPlayerEffect.instance.ToggleP2Mult(true);
        }

        if (!LevelManager.Instance.isComboEnabled) 
        {
            LevelManager.Instance.comboEffectAnimator.SetTrigger("Enable");
        }

        LevelManager.Instance.isComboEnabled = true;

        yield return new WaitForSeconds(time);

        if (isP1)
        {
            ShowPlayerEffect.instance.ToggleP1Mult(false);
        }
        else
        {
            ShowPlayerEffect.instance.ToggleP2Mult(false);
        }

        if (LevelManager.Instance.isComboEnabled)
        {
            LevelManager.Instance.comboEffectAnimator.SetTrigger("Disable");
        }

        LevelManager.Instance.isComboEnabled = false;
        isStreakEffect = false;

    }
}
