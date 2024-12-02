using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;
    
    private void OnEnable()
    {
        CurrencyManager.Instance.OnCurrencyChanged += OnCurrencyChanged;
    }
    
    private void OnDisable()
    {
        CurrencyManager.Instance.OnCurrencyChanged -= OnCurrencyChanged;
    }
    
    private void OnCurrencyChanged()
    {
        _currencyText.text = $"Currency: {CurrencyManager.Instance.GetCurrency()}";
    }
}
