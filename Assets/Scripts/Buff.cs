using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    private bool enabled = true;
    
    //Subject to change
    public enum BuffType
    {
        Regen,
        Speed,
        Damage,
        Pierce,
        AOEAbility,
        RicochetAbility,
        EnemyRepellant,
        Shield,
        Lifesteal


    }
    public Buff(BuffType type)
    {
        switch (type)
        {
            case BuffType.Health:

                break;
            case BuffType.Speed:

                break;
            case BuffType.Damage:

                break;
            case BuffType.AOEAbility:

                break;
            case BuffType.RicochetAbility:

                break;
            case BuffType.EnemyRepellant:

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

    public static List<Buff> newList()
    {
        return new List<Buff> {
            new Buff(BuffType.Health),
            new Buff(BuffType.Speed),
            new Buff(BuffType.Damage)
    };
    }
}
