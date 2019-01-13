using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HealthBarMB : MonoBehaviour
{
    [SerializeField] private float minLimit;
    [SerializeField] private float maxLimit;
    [SerializeField] private float health;
    [SerializeField] private Slider slider;
    [SerializeField] private Text textRend;

    public System.Action OnHealthZero;

    private void Awake()
    {
        Assert.IsTrue(slider != null);
        Assert.IsTrue(textRend != null);
        // Set initial UI values
        slider.minValue = 0;
        slider.maxValue = maxLimit;
        RefreshUI();
    }

    public void AddHealth(int value)
    {
        // Add health up to max limit
        if (health + value < maxLimit)
            health += value;
        else health = maxLimit;
        RefreshUI();
    }

    public void RemoveHealth(int value)
    {
        // Remove health down to min limit
        if (health - value > minLimit)
            health -= value;
        else health = minLimit;
        RefreshUI();
    }

    private void RefreshUI()
    {
        // Update UI view to reflect current values
        slider.value = health;
        textRend.text = health.ToString() + " / " + maxLimit.ToString();
    }

    public void CheckOnHealthZero()
    {
        // Calls the event if health is less than zero
        if (health <= 0 && OnHealthZero != null)
            OnHealthZero();
    }
}