using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public Rigidbody2D playerRb;

    private Vector2 inputValue;

    private GameObject bubbleInteracted;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    public void OnInputMove(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<Vector2>();

        Debug.Log("input value : " + inputValue);

        if (inputValue == Vector2.zero)
        {
            playerRb.velocity = Vector2.zero;
        }
    }
    public void OnInputPopping(InputAction.CallbackContext context)
    {
        bool isPress = context.ReadValueAsButton();
        if (isPress)
        {
            OnPoppingWithBubble();
        }
    }

    private void FixedUpdate() 
    {
        OnMoveUpdate();
    }

    private void OnMoveUpdate()
    {
        if (inputValue == Vector2.zero) return;
        playerRb.velocity = inputValue * playerData.Speed;

        // Change Rotation
        Quaternion lookRot = Quaternion.LookRotation(transform.forward, inputValue);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, .1f);
    }
    private void OnPoppingWithBubble()
    {
        if (bubbleInteracted == null) return;

        Bubble bubble = bubbleInteracted.GetComponent<Bubble>();

        bubble.OnPopping(this);
    }

    #region On Checking Collider
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Collide : " + other.gameObject.name);
        if (other.gameObject.layer == 6)
        {
            bubbleInteracted = other.gameObject;
        }
    }
    #endregion


    #region On Player Popping Bubble
    public void OnPlayerPoppingBubble(Player player, EnumManager.BubbleType type, Vector3 lastBubblePosition)
    {
        playerData.point++;
        if (playerData.isStreakEffect)
        {
            playerData.point++;
        }

        if (type == EnumManager.BubbleType.NonEffect) return;

        OnPlayerEffect(type, lastBubblePosition);
    }
    public void OnPlayerEffect(EnumManager.BubbleType bubbleType, Vector3 lastBubblePosition)
    {
        switch (bubbleType)
        {
            case EnumManager.BubbleType.Speed:
                playerData.isSpeedEffect = true;
                StartCoroutine(playerData.ChangeSpeed(5, playerData.Speed * 2, () => 
                {
                    playerData.isSpeedEffect = false;
                }));
                break;
            case EnumManager.BubbleType.Stun:
                StunOtherPlayer();
                break;
            case EnumManager.BubbleType.Bomb:
                BlastPlayerArround(lastBubblePosition);

                break;
            case EnumManager.BubbleType.Streak:
                StartCoroutine(playerData.GetStreakEffect(5));
                
                break;
        }
    }

    public void StunOtherPlayer()
    {
        Player[] players = GameObject.FindObjectsOfType<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log(players[i].name);
            if (players[i] == this) continue;

            Player player = players[i];
            player.playerData.isStunEffect = true;
            player.StartCoroutine(player.playerData.ChangeSpeed(5, 0, () =>
            {
                player.playerData.isStunEffect = false;
            }));
        }
    }
    public void BlastPlayerArround(Vector3 lastBubblePosition)
    {
        Player[] players = GameObject.FindObjectsOfType<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            Player player = players[i];
            
            float distance = Vector3.Distance(player.transform.position, lastBubblePosition);
            
            if (distance <= 3)
            {
                Vector3 direction = player.transform.position - lastBubblePosition;
                Debug.DrawRay(player.transform.position, direction, Color.red, 100);
                player.playerRb.AddForce(direction * 10, ForceMode2D.Impulse);
            }
        }
    }
    #endregion
}
