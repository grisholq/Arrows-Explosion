using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FirendsAmountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentAmount;
    [SerializeField] private TextMeshProUGUI _minAmount;
    [SerializeField] private UnityEvent _updated;

    public void ShowAmount(int amount, int minAmount)
    {
        _currentAmount.text = amount.ToString();
        _minAmount.text = "min " + minAmount.ToString();
        _updated.Invoke();
    }    
}