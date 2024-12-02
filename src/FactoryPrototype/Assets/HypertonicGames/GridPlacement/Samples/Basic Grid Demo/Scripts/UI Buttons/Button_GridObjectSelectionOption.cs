using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hypertonic.GridPlacement.Example.BasicDemo
{
    [RequireComponent(typeof(Button))]
    public class Button_GridObjectSelectionOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        public static event System.Action<GameObject> OnOptionSelected;

        [SerializeField] private GameObject _gridObjectToSpawnPrefab;

        private Button _button;
        private PlaceableItem _placeableItem;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _placeableItem = _gridObjectToSpawnPrefab.GetComponent<PlaceableItem>();
        }

        private void OnEnable()
        {
            CurrencyManager.Instance.OnCurrencyChanged += OnCurrencyChanged;
            UpdateButtonInteractable();
        }

        private void OnDisable()
        {
            CurrencyManager.Instance.OnCurrencyChanged -= OnCurrencyChanged;
        }

        private void Start()
        {
            Button button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(HandleButtonClicked);
            }

            // Objenin degerini yazdir
            _priceText.text = $"{_placeableItem.Value}$";
        }

        private void HandleButtonClicked()
        {
            if (_gridObjectToSpawnPrefab == null)
            {
                Debug.LogError("Error. No prefab assigned to spawn on this selection option");
            }

            GameObject objectToPlace = Instantiate(_gridObjectToSpawnPrefab, GridManagerAccessor.GridManager.GetGridPosition(), new Quaternion());

            objectToPlace.name = _gridObjectToSpawnPrefab.name;

            if (!objectToPlace.TryGetComponent(out ExampleGridObject gridObject))
            {
                objectToPlace.AddComponent<ExampleGridObject>();
            }

            OnOptionSelected?.Invoke(objectToPlace);

            GridManagerAccessor.GridManager.EnterPlacementMode(objectToPlace);
        }
        
        private void OnCurrencyChanged()
        {
            UpdateButtonInteractable();
        }

        private void UpdateButtonInteractable()
        {
            _button.interactable = CurrencyManager.Instance.CanBuy(_placeableItem.Value);
        }
    }
}