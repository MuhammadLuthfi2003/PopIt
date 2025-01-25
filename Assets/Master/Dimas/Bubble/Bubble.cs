using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BubbleAnimations
{
    [System.Serializable]
    public class BubbleAnimationStateByHP
    {
        public int hpLevel;
        public string stateName;
    }

    public BubbleAnimationStateByHP[] bubbleAnimationStateByHP;
}

public class Bubble : MonoBehaviour
{
    public BubbleAnimations animationState;
    public EnumManager.BubbleType bubbleType;

    public float defaultHP;
    public float currentHP;

    private void Start() {
        currentHP = defaultHP;
    }
    public void OnPopping(Player player)
    {
        Debug.Log("On Popping");
        currentHP--;

        if (currentHP <= 0)
        {
            player.OnPlayerPoppingBubble(player, bubbleType, transform.position);
            Destroy(gameObject);
        }
    }
}
