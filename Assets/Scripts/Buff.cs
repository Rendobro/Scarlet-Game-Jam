using System.Collections.Generic;
using UnityEngine;

public class Buff
{

    public enum BuffType
    {
        Health,
        Speed,
        Damage
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
