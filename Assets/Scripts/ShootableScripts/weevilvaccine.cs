using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weevilvaccine : MonoBehaviour
{
    [SerializeField] 
    private float _weevilvaccineHorizontalSpeed = 5f;

    [SerializeField] 
    private float _weevilvaccineVerticalSpeed = 3f;
    
    
    
    void Start()
    {
        transform.Rotate(90f, 0f, 0f);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.right) * _weevilvaccineHorizontalSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _weevilvaccineVerticalSpeed * Time.deltaTime, Space.World);
        if (transform.position.x > 12f)
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
