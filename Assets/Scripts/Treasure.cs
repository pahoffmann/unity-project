using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private float _treasurePowerUpSpeed = 5f;


    public AudioClip moneysound;
    
    // Start is called before the first frame update
    void Start()
    {
        //bling = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, -_treasurePowerUpSpeed * Time.deltaTime, 0f, Space.World);
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
            Debug.Log("moneysound should be played");
            AudioSource.PlayClipAtPoint(moneysound, transform.position);
            Debug.Log("treasure hit player");
            
            //hier muss noch die Funktion hin, die dem Score mehr Punkte gibt (das Power up gibt einem Punkte)
            
            
            
            Destroy(this.gameObject);
        }
        
    }
   

}
