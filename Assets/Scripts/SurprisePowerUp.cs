using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SurprisePowerUp : MonoBehaviour
{
    [SerializeField] private Coroner _coroner;
    [SerializeField] private Shield _shield;
    [SerializeField] private float _surprisePowerUpSpeed = 3f;
    private float _randomInt;
    [FormerlySerializedAs("bling2")] public AudioClip goodSurprise;
    public AudioClip badSurprise;

    
    
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
            Debug.Log("SurprisePowerUp SideShoots was chosen" + _randomInt);
            GameObject.FindObjectOfType<Player>().ActivateSideShoots(5f);
            AudioSource.PlayClipAtPoint(goodSurprise, transform.position);
        }

        else if (_randomInt <= 2 && _randomInt > 1)
        {
            Debug.Log("SurprisePowerUp Shield was chosen" + _randomInt);
            GameObject.FindObjectOfType<Player>().InstantShield();
            AudioSource.PlayClipAtPoint(goodSurprise, transform.position);
            
        }

        else if (_randomInt <= 3 && _randomInt > 2)
        {
            Debug.Log("SurprisePowerUp SlowCorona was chosen" + _randomInt);
            AudioSource.PlayClipAtPoint(badSurprise, transform.position);
            //_coroner.SlowerCorona();
            GameObject.FindObjectOfType<Player>().Slower();
        }

        else if (_randomInt <=4 && _randomInt > 3)
        {
            Debug.Log("SurprisePowerUp FasterCorona was chosen" + _randomInt);
            AudioSource.PlayClipAtPoint(badSurprise, transform.position);
            //_coroner.FasterCorona();
            GameObject.FindObjectOfType<Player>().Faster();
        }

        else
        {
            Debug.Log("SurprisePowerUp Nr0 was chosen...something strange happened" + _randomInt);
        }
    }
    
}
