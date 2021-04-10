using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUp : MonoBehaviour
{
    private float _heartPowerUpSpeed = 3f;


    public AudioClip nicesound;

    private Player _player;
    
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, -_heartPowerUpSpeed * Time.deltaTime, 0f, Space.World);
        if (transform.position.y < -6f)
        {
            Debug.Log("Bottom boundary has been called");
            Destroy(this.gameObject);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        // collision handling
        if (other.CompareTag("Player"))
        {
            Debug.Log("nicesound should be played");
            AudioSource.PlayClipAtPoint(nicesound, transform.position);
            Debug.Log("heart hit player");
            
            //hier muss noch die Funktion hin, die das Leben um 1 erhöht
            other.GetComponent<Player>().OneMoreLife();
            
            
            
            Destroy(this.gameObject);
        }
        
    }

}
