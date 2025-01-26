using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// public class AnimationClipContainer
// {
//     public class AnimationClips
//     {

//     }
// }

[System.Serializable]
public class AnimationClipContainer
{
    [System.Serializable]
    public class AnimationClips 
    {
        public string onEndKey;
        public UnityEvent EventToExecute;
    }

    public AnimationClips[] CurrentAnimationState;
    
    public AnimationClips GetClipsEvent(string onEndKey)
    {
        for (int i = 0; i < CurrentAnimationState.Length; i++)
        {
            if (CurrentAnimationState[i].onEndKey == onEndKey)
            {
                return CurrentAnimationState[i];
            }
        }

        return null;
    }
}

public class AnimationClipEventCostum : MonoBehaviour
{
    public AnimationClipContainer animationClipContainer;

    public void AnimationClipContainer(string keyName)
    {
        animationClipContainer.GetClipsEvent(keyName).EventToExecute?.Invoke();
    }

    public void DestroyEvent(Transform target)
    {
        Destroy(target.gameObject);
    }
}
