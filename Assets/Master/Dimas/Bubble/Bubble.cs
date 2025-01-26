using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BubbleColorByHP
{
    [System.Serializable]
    public class BubbleColor 
    {
        public int HP;
        public Color color;
    }

    public BubbleColor[] CurrentAnimationState;
    
    public BubbleColor GetColorByHP(int hp)
    {
        for (int i = 0; i < CurrentAnimationState.Length; i++)
        {
            if (CurrentAnimationState[i].HP == hp)
            {
                return CurrentAnimationState[i];
            }
        }

        return null;
    }
}

public class Bubble : MonoBehaviour
{
    public EnumManager.BubbleType bubbleType;

    public int defaultHP;
    public int currentHP;

    public SpriteRenderer bubbleRenderer;
    public Animator bubbleAnimator;
    public BubbleColorByHP bubbleColorByHP;
    public AnimationStateContainer bubbleAnimationStateContainer;

    public Rigidbody2D bubbleRb;

    private Player currentPlayerPopping;

    private void Awake() 
    {
        bubbleRb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        currentHP = defaultHP;

        UpdateDisplayByHP();
    }
    public void OnPopping(Player player)
    {
        Debug.Log("On Popping");
        currentHP--;

        // Pecah dulu baru ganti warna
        // UpdateDisplayByHP();

        currentPlayerPopping = player;
        bubbleAnimator.Play(bubbleAnimationStateContainer.GetAnimationStateNameByAnimationName("Popping"));
    }
    public void SetForce(Vector3 direction, ForceMode2D forceMode)
    {
        bubbleRb.AddForce(direction, forceMode);
    }

    public void OnEndPoppingAnimation()
    {
        UpdateDisplayByHP();

        if (currentHP <= 0)
        {
            LevelManager.Instance.bubbleGenerator.RemoveAndDestroyBubbleOnPopping(this);
            
            currentPlayerPopping.OnPlayerPoppingBubble(currentPlayerPopping, defaultHP, bubbleType, transform.position);
        }else
        {
            Vector3 direction = transform.position - currentPlayerPopping.transform.position;
            SetForce(direction, ForceMode2D.Impulse);
        }
        
        currentPlayerPopping = null;
    }
    private void UpdateDisplayByHP()
    {
        BubbleColorByHP.BubbleColor bubbleColor = bubbleColorByHP.GetColorByHP(currentHP);

        if (bubbleColor == null) return;
        bubbleRenderer.color = bubbleColor.color;
    }
}
