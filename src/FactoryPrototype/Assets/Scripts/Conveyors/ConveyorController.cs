using System;
using System.Collections.Generic;
using UnityEngine;

namespace Conveyors
{
    public class ConveyorController : MonoBehaviour
    {
        [SerializeField] [Range(1, 10)] private float _pushForce;
        [SerializeField] private bool _isOperating;

        private List<Transform> _pushObjects = new List<Transform>();

        private void Update()
        {
            if (_isOperating)
            {
                foreach (Transform pushObject in _pushObjects)
                {
                    pushObject.Translate(transform.forward * (_pushForce * Time.deltaTime));
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("On trigger çalıştı");
            if (other.CompareTag("Pushable"))
            {
                Debug.Log("Pushable tagi çalıştı");
                if (!_pushObjects.Contains(other.transform))
                {
                    Debug.Log("listeye eklendi");
                    _pushObjects.Add(other.transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Pushable"))
            {
                if (_pushObjects.Contains(other.transform))
                {
                    _pushObjects.Remove(other.transform);
                }
            }
        }
    }
}
