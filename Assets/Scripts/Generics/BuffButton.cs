using System;
using UnityEngine;
using UnityEngine.UI;

public class BuffButton : MonoBehaviour
{
    [SerializeField] private Buff.BuffType buffType;
    private bool allowedPress = true;
    [SerializeField] private Transform childTransform;

    private void Awake()
    {
        if (childTransform == null)
            childTransform = transform.GetChild(0);
    }

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        if (toggle != null && !toggle.isOn)
        {
            childTransform.localPosition = Vector3.zero;
        }
    }

    public void OnClick(bool active)
    {
        Debug.Log($"BuffButton for {buffType} clicked, active: {active}");
        Debug.Log($"Allowed to press: {allowedPress}");
        if (!allowedPress) return;

        childTransform.localPosition = !active ? Vector3.zero : new Vector3(0, -1000, 0);

        Buff buff = Player.Instance.activeBuffs?.Find(b => b.buffType == buffType);
        if (buff != null)
        {
            if (active) buff.Activate();
            else buff.Deactivate();
        }
    }

    public void DisallowPress() => allowedPress = false;
    public void AllowPress() => allowedPress = true;
}
