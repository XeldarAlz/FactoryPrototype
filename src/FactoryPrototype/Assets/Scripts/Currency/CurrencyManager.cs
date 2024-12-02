using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    private const string CurrencyKey = "Currency";

    private int _currentCurrency
    {
       get => PlayerPrefs.GetInt(CurrencyKey, 400);
       set => PlayerPrefs.SetInt(CurrencyKey, value);
    }
    public int GetCurrency() => _currentCurrency;
    
    public delegate void CurrencyDelegate();
    public event CurrencyDelegate OnCurrencyChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        OnCurrencyChanged?.Invoke();
    }

    public void IncreaseCurrency(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Increase currency cannot be less or equal to zero");
            return;
        }

        checked
        {
            _currentCurrency += amount;
            OnCurrencyChanged?.Invoke();
        }
    }
    
    public void DecreaseCurrency(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Increase currency cannot be less or equal to zero");
            return;
        }
        
        checked
        {
            if (_currentCurrency - amount >= 0)
            {
                _currentCurrency -= amount;
                OnCurrencyChanged?.Invoke();
            }
            else
            {
                // Yeterli para yok
                Debug.LogWarning("Not enough currencies left");
            }
        }
    }

    public bool CanBuy(int amount)
    {
        return _currentCurrency >= amount;
    }

    // TEST İÇİN, SİLİNECEK 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseCurrency(100);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            DecreaseCurrency(50);
        }
    }
}