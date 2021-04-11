using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private float _treasurePowerUpSpeed = 5f;


    public AudioClip moneysound;

    [SerializeField] private int _score = 20;

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
            
            //how many points should the treasure give you? I set it to 20
            
            GameObject.FindObjectOfType<UIManager>().AddScore(_score);
            
            
            Destroy(this.gameObject);
        }
        
    }
   

}
