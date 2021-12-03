using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoltsAmountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private UnityEvent _updated;

    public void ShowAmount(int amount)
    {
        _amountText.text = amount.ToString();
        _updated.Invoke();
    }
}