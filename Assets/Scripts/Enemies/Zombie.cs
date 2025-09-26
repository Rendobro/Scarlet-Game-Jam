using UnityEngine;

[System.Serializable]
public class Zombie : Enemy
{
    [ContextMenu("Test Death")]
    public void Die()
    {
        Perish();
    }
    [ContextMenu("Test Damage (5)")]
    public new void Damage()
    {
        Health -= 5;
    }
}