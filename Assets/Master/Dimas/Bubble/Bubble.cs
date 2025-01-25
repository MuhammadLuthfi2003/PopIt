using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public EnumManager.BubbleType bubbleType;

    public int defaultHP;
    public int currentHP;

    public Rigidbody2D bubbleRb;

    private void Awake() 
    {
        bubbleRb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        currentHP = defaultHP;
    }
    public void OnPopping(Player player)
    {
        Debug.Log("On Popping");
        currentHP--;

        if (currentHP <= 0)
        {
            LevelManager.Instance.bubbleGenerator.RemoveAndDestroyBubbleOnPopping(this);
            
            player.OnPlayerPoppingBubble(player, defaultHP, bubbleType, transform.position);
        }else
        {
            Vector3 direction = transform.position - player.transform.position;
            SetForce(direction, ForceMode2D.Impulse);
        }
    }
    public void SetForce(Vector3 direction, ForceMode2D forceMode)
    {
        bubbleRb.AddForce(direction, forceMode);
    }
}
