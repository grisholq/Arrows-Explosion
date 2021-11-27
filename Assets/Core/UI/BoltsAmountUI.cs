using TMPro;
using UnityEngine;

public class BoltsAmountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;

    public void ShowAmount(int amount, int maxAmount)
    {
        _amountText.text = amount.ToString() + "/" + maxAmount.ToString();
    }
}