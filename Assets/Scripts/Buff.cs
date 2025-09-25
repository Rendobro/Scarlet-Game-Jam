using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buff
{
    private bool enabled = true;
    public BuffType buffType;


    //Subject to change
    public enum BuffType
    {
        Regen,
        Speed,
        Damage,
        Pierce,
        AOEAbility,
        RicochetAbility,
        EnemyDebuff,
        Shield,
        Lifesteal


    }
    public Buff(BuffType type)
    {
        Enable();
        buffType = type;
    }

    public static List<Buff> initializeBuffs()
    {
        return new List<Buff> {
            new Buff(BuffType.Regen),
            new Buff(BuffType.Speed),
            new Buff(BuffType.Damage),
            new Buff(BuffType.AOEAbility),
            new Buff(BuffType.RicochetAbility),
            new Buff(BuffType.EnemyDebuff),
            new Buff(BuffType.Shield),
            new Buff(BuffType.Lifesteal)
        };
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }

    public void Activate()
    {
        float defaultSpeed = 5f;
        enabled = true;
        switch (buffType)
        {
            case BuffType.Regen:
                
                break;
            case BuffType.Speed:
                foreach (IBuffTarget target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffTarget>())
                {
                    target.BuffSpeed(defaultSpeed);
                }
                break;
            case BuffType.Damage:

                break;
            case BuffType.AOEAbility:

                break;
            case BuffType.RicochetAbility:

                break;
            case BuffType.EnemyDebuff:

                break;
            case BuffType.Shield:

                break;
            case BuffType.Lifesteal:

                break;
            default:
                Debug.LogError("Buff type not recognized");
                break;
        }
    }
}
