using UnityEngine;

public class PlaceableItem : MonoBehaviour
{
    [SerializeField] private int _value;
    public int Value => _value;

    public bool IsPlaced { get; set; }
}
