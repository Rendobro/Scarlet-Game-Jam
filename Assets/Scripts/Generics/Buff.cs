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
            new Buff(BuffType.Pierce),
            new Buff(BuffType.AOEAbility),
            new Buff(BuffType.RicochetAbility),
            new Buff(BuffType.EnemyDebuff),
            new Buff(BuffType.Shield),
            new Buff(BuffType.Lifesteal)
        };
    }

    public bool IsEnabled() => enabled;
    public void Enable() => enabled = true;
    public void Disable() => enabled = false;
    public void Activate()
    {
        float defaultSpeedBuff = 3f;
        int defaultDamageBuff = 2;
        int defaultShieldBuff = 5;
        int defaultPierceBuff = 1;
        switch (buffType)
        {
            case BuffType.Regen:
                Player.Instance.EnableRegen();
                break;
            case BuffType.Speed:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.BuffSpeed(defaultSpeedBuff);
                }
                break;
            case BuffType.Damage:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.BuffDamage(defaultDamageBuff);
                }
                break;
            case BuffType.Pierce:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.BuffPierce(defaultPierceBuff);
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
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.SetShield(defaultShieldBuff);
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
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.BuffSpeed(strength);
                }
                break;
            case BuffType.Damage:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.BuffDamage((int)Mathf.Floor(strength));
                }
                break;
            case BuffType.Pierce:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.BuffPierce((int)Mathf.Floor(strength));
                }
                break;
            case BuffType.Lifesteal:
                Player.Instance.EnableLifesteal();
                break;
            case BuffType.Shield:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.SetShield((int)Mathf.Floor(strength));
                }
                break;
            default:
                Debug.LogError("Buff type not recognized or doesn't support strength parameter");
                Activate();
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
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.ResetSpeed();
                }
                break;
            case BuffType.Damage:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.ResetDamage();
                }
                break;
            case BuffType.Pierce:
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
                {
                    target.ResetPierce();
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
                foreach (IBuffable target in GameObject
                .FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IBuffable>())
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
