using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private float _shieldLifes = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coroner"))
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
