using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void InstantShield()
    {
        Instantiate(this,transform.position, Quaternion.identity);
        this.transform.parent = _player.transform;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coroner"))
        {

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
