using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprisePowerUp : MonoBehaviour
{
    private float _surprisePowerUpSpeed = 3f;

    private float _randomInt;
    

    public AudioClip bling2;
    
    // Start is called before the first frame update
    void Start()
    {
        //bling = GetComponent<AudioSource>();
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
            Debug.Log("bling should be played");
            AudioSource.PlayClipAtPoint(bling2, transform.position);
            SurpriseSurprise();
            //Debug.Log("surprise hit player");
            Destroy(this.gameObject);
        }
        
    }

   // this function selects a random PowerUp as a surprise
    private void SurpriseSurprise()
    {
        _randomInt = Random.Range(0f, 4f);

        if (_randomInt <= 1)
        {
            Debug.Log("SurprisePowerUp Nr1 was chosen" + _randomInt);
            //needs to be implemented in player so it can be called:  _player.ActivateSideShoots();
            
        }

        else if (_randomInt <= 2 && _randomInt > 1)
        {
            Debug.Log("SurprisePowerUp Nr2 was chosen" + _randomInt);
            //needs to be implemented in player so it can be called:  _player.NoVaccine();
        }

        else if (_randomInt <= 3 && _randomInt > 2)
        {
            Debug.Log("SurprisePowerUp Nr3 was chosen" + _randomInt);
        }

        else if (_randomInt <=4 && _randomInt > 3)
        {
            Debug.Log("SurprisePowerUp Nr4 was chosen" + _randomInt);
        }

        else
        {
            Debug.Log("SurprisePowerUp Nr0 was chosen...something strange happened" + _randomInt);
        }
    }
    
}
