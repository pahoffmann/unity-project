using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devilvaccine : MonoBehaviour
{
    [SerializeField] 
    private float _devilvaccineHorizontalSpeed = 5f;

    [SerializeField] 
    private float _devilvaccineVerticalSpeed = 3f;
    
    
    
    void Start()
    {
        transform.Rotate(90f, 0f, 0f);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.left) * _devilvaccineHorizontalSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _devilvaccineVerticalSpeed * Time.deltaTime, Space.World);
        if (transform.position.x < -12f)
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
