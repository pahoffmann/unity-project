using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private float _shieldLifes = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coroner") || other.CompareTag("devilvaccine") ||
            other.CompareTag("evilvaccine") || other.CompareTag("weevilvaccine") ||
            other.CompareTag("Boss") || other.CompareTag("FriendlySpaceship"))
        {
            Destroy(other.gameObject);
            
            _shieldLifes--;
            if (_shieldLifes == 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
