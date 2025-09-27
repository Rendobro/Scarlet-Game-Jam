using UnityEngine;

public class BuffButton : MonoBehaviour
{
    [SerializeField] private Buff.BuffType buffType;
    public void OnClick()
    {
        Player.Instance.playerBuffs
                            .Find(b => b.buffType == buffType)
                            .Activate();
    }
}
