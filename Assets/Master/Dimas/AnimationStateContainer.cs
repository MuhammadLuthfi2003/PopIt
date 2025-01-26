
[System.Serializable]
public class AnimationStateContainer
{
    [System.Serializable]
    public class AnimationStates
    {
        public string animationName;
        public string animationStateName;
    }

    public AnimationStates[] CurrentAnimationState;
    
    public string GetAnimationStateNameByAnimationName(string animationName)
    {
        for (int i = 0; i < CurrentAnimationState.Length; i++)
        {
            if (CurrentAnimationState[i].animationName == animationName)
            {
                return CurrentAnimationState[i].animationStateName;
            }
        }

        return string.Empty;
    }
}