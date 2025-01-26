using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public Rigidbody2D playerRb;
    public Animator playerAnimator;
    public Transform playerEffectParentPosition;

    public AnimationStateContainer playerAnimationContainer;

    private Vector2 inputValue;

    private List<GameObject> bubblesInteracted = new List<GameObject>();
    private bool IsHasPopping;

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

            return;
        }
    }
    public void OnInputPopping(InputAction.CallbackContext context)
    {
        bool isPress = context.ReadValueAsButton();
        if (isPress)
        {
            Debug.Log("Pressing");
            if (IsHasPopping) return;

            OnPoppingWithBubble();
            SFXPlayer.instance.PlaySwingSFX();

            StartCoroutine(PoppingDelay());
        }
    }
    private IEnumerator PoppingDelay()
    {
        IsHasPopping = true;
        yield return new WaitForSeconds(1);
        IsHasPopping = false;
    }

    private void FixedUpdate() 
    {
        OnMoveUpdate();
    }

    private void OnMoveUpdate()
    {
        if (inputValue == Vector2.zero) return;
        playerRb.velocity = inputValue * playerData.RuntimeSpeed;

        // Change Rotation
        Quaternion lookRot = Quaternion.LookRotation(transform.forward, inputValue);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, .25f);
    }
    private void OnPoppingWithBubble()
    {
        playerAnimator.Play(playerAnimationContainer.GetAnimationStateNameByAnimationName("Attack"));

        if (bubblesInteracted == null || bubblesInteracted.Count <= 0) return;

        Bubble bubble = bubblesInteracted[0].GetComponent<Bubble>();
        bubble.OnPopping(this);
    }

    #region On Checking Collider
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Collide : " + other.gameObject.name);
        if (other.gameObject.layer == 6)
        {
            bubblesInteracted.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        Debug.Log("Collide : " + other.gameObject.name);
        if (other.gameObject.layer == 6)
        {
            bubblesInteracted.Remove(other.gameObject);
        }
    }
    #endregion


    #region On Player Popping Bubble
    public void OnPlayerPoppingBubble(Player player, int hp, EnumManager.BubbleType type, Vector3 lastBubblePosition)
    {
        playerData.point += hp;
        SFXPlayer.instance.PlayBubblePopSFX();
        if (playerData.isStreakEffect)
        {
            playerData.point += hp * 2;
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

                if (playerData.isP1)
                {
                    ShowPlayerEffect.instance.ToggleP1Speed(true);
                }
                else
                {
                    ShowPlayerEffect.instance.ToggleP2Speed(true);
                }

                if (playerData.SpeedCorotine != null) StopCoroutine(playerData.SpeedCorotine);
                playerData.SpeedCorotine = StartCoroutine(playerData.ChangeSpeed(5, playerData.DefaultSpeed * 2, () => 
                {
                    playerData.isSpeedEffect = false;
                }));
                SFXPlayer.instance.PlaySpeedSFX();
                break;
            case EnumManager.BubbleType.Stun:
                StunOtherPlayer();
                SFXPlayer.instance.PlayStunSFX();
                break;
            case EnumManager.BubbleType.Bomb:
                BlastPlayerArround(lastBubblePosition);
                SFXPlayer.instance.PlayExplosionSFX();
                break;
            case EnumManager.BubbleType.Streak:
                if (playerData.StreakCoroutine != null) StopCoroutine(playerData.StreakCoroutine);

                    playerData.StreakCoroutine = StartCoroutine(playerData.GetStreakEffect(5));
                
                break;
        }
    }

    public void StunOtherPlayer()
    {
        Player[] players = GameObject.FindObjectsOfType<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == this) continue;

            Debug.Log(players[i].name);
            Player player = players[i];
            
            if (playerData.isP1)
            {
                ShowPlayerEffect.instance.ToggleP2Stun(true);
            }
            else
            {
                ShowPlayerEffect.instance.ToggleP1Stun(true);
            }

            // Effect
            GameObject effectObject = EffectManager.Instance.CreateEffect("Freeze", player.playerEffectParentPosition.position, childTransform: player.playerEffectParentPosition);

            player.playerData.isStunEffect = true;
            if (player.playerData.SpeedCorotine != null) player.StopCoroutine(player.playerData.SpeedCorotine);
            player.playerData.SpeedCorotine = player.StartCoroutine(player.playerData.ChangeSpeed(5, player.playerData.DefaultSpeed / 2, () =>
            {
                player.playerData.isStunEffect = false;
                Destroy(effectObject.gameObject);
            }));
        }
    }
    public void BlastPlayerArround(Vector3 lastBubblePosition)
    {
        GameObject effectObject = EffectManager.Instance.CreateEffect("Bomb", lastBubblePosition, AnimationEventKey: "OnEndBombEffect", AnimationEventAction: (x) => {
            Destroy(x.gameObject);
        });

        Player[] players = GameObject.FindObjectsOfType<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            Player player = players[i];
            
            float distance = Vector3.Distance(player.transform.position, lastBubblePosition);
            
            if (distance <= 3)
            {
                Vector3 direction = player.transform.position - lastBubblePosition;

                player.playerRb.AddForce(direction * 5, ForceMode2D.Impulse);
            }
        }

        Bubble[] bubbles = LevelManager.Instance.bubbleGenerator.Bubbless.ToArray();
        for (int i = 0; i < bubbles.Length; i++)
        {
            Bubble bubble = bubbles[i];
            float distance = Vector3.Distance(bubble.transform.position, lastBubblePosition);
            
            if (distance > 3)
            {
                continue;
            }

            Vector3 direction = bubble.transform.position - lastBubblePosition;
            bubble.SetForce(direction * 5, ForceMode2D.Impulse);

            bubble.OnPopping(this);
        }
    }
    #endregion
}
