using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectBubbleContainer
{
    [System.Serializable]
    public class EffectBubble 
    {
        public string effectName;
        public GameObject effectObjectToSpawn;
    }

    public EffectBubble[] CurrentAnimationState;
    
    public EffectBubble GetEffectByName(string effectName)
    {
        for (int i = 0; i < CurrentAnimationState.Length; i++)
        {
            if (CurrentAnimationState[i].effectName == effectName)
            {
                return CurrentAnimationState[i];
            }
        }

        return null;
    }
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public EffectBubbleContainer effectBubbleContainer;

    private void Awake() {
        Instance = this;
    }

    public GameObject CreateEffect(string effectName, Vector3 position, Transform childTransform = null)
    {
        EffectBubbleContainer.EffectBubble effectBubble = effectBubbleContainer.GetEffectByName(effectName);
        GameObject effect = Instantiate(effectBubble.effectObjectToSpawn);

        effect.transform.position = position;
        effect.transform.SetParent(childTransform);

        return effect;
    }
}
