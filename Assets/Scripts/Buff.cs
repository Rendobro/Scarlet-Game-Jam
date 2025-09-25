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

    public void Enable() => enabled = true;
    public void Disable() => enabled = false;
    public void Activate()
    {
        float defaultSpeedBuff = 5f;
        float defaultDamageBuff = 5f;
        float defaultShieldBuff = 3f;
        switch (buffType)
        {
            case BuffType.Regen:
                Player.Instance.EnableRegen();
                break;
            case BuffType.Speed:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.BuffSpeed(defaultSpeed);
                }
                break;
            case BuffType.Damage:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.BuffDamage(defaultSpeedBuff);
                }
                break;
            case BuffType.AOEAbility:
                Player.Instance.EnableAOE();
                break;
            case BuffType.RicochetAbility:
                Player.Instance.EnableRicochet();
                break;
            case BuffType.EnemyDebuff:
                Player.Instance.EnableEnemyDebuff();
                break;
            case BuffType.Shield:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.SetShield(defaultShielddBuff);
                }
                break;
            case BuffType.Lifesteal:
                Player.Instance.EnableLifesteal();
                break;
            default:
                Debug.LogError("Buff type not recognized");
                break;
        }
    }
    public void Activate(float strength)
    {
        if (!enabled)
        {
            Debug.LogError("Buff is disabled, cannot activate");
            return;
        }
        if (strength <= 0)
        {
            Debug.LogError("Strength must be a positive value./n Using default strength values instead.");
            Activate();
            return;
        }
        switch (buffType)
        {
            case BuffType.Speed:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.BuffSpeed(strength);
                }
                break;
            case BuffType.Damage:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.BuffDamage(strength);
                }
                break;
            case BuffType.Shield:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.SetShield(strength);
                }
                break;
            default:
                Debug.LogError("Buff type not recognized or doesn't support strength parameter");
                break;
        }
    }
    public void Deactivate()
    {
        Disable();
        switch (buffType)
        {
            case BuffType.Regen:
                Player.Instance.DisableRegen();
                break;
            case BuffType.Speed:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.ResetSpeed();
                }
                break;
            case BuffType.Damage:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.ResetDamage();
                }
                break;
            case BuffType.AOEAbility:
                Player.Instance.DisableAOE();
                break;
            case BuffType.RicochetAbility:
                Player.Instance.DisableRicochet();
                break;
            case BuffType.EnemyDebuff:
                Player.Instance.DisableEnemyDebuff();
                break;
            case BuffType.Shield:
                foreach (IBuffFriendly target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffFriendly>())
                {
                    target.ResetShield();
                }
                break;
            case BuffType.Lifesteal:
                Player.Instance.DisableLifesteal();
                break;
            default:
                Debug.LogError("Buff type not recognized");
                break;
        }
    }
}
