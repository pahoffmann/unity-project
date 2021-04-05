using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evilvaccine : MonoBehaviour
{
    [SerializeField] 
    private float _evilvaccineSpeed = 5f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.down) * _evilvaccineSpeed * Time.deltaTime);
            if (transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Vaccine"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("UVLight"))
        {
            Destroy(this.gameObject);
        }
    }
}
