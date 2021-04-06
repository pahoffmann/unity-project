using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprisePowerUp : MonoBehaviour
{
    private float _surprisePowerUpSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, -_surprisePowerUpSpeed * Time.deltaTime, 0f, Space.World);
        
        BottomBoundary();
    }

    private void BottomBoundary()
    {
        //Debug.Log("Bottom boundary has been called");
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // collision handling
        if (other.CompareTag("Player"))
        {
            //Debug.Log("surprise hit player");
            Destroy(this.gameObject);
        }
        
    }
}
