using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _powerUpDuration = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        TranslatePowerUp();
        RotatePowerUp();
        CheckOutOfBounds();
    }

    /// <summary>
    /// Collision handling
    /// activates UVLight if collision with player
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // activate UVLight in Player script if PowerUp is collected
            other.GetComponent<Player>().ActivateUVLight(_powerUpDuration);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// translation
    /// </summary>
    private void TranslatePowerUp()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
    }
    
    /// <summary>
    /// rotation
    /// </summary>
    void RotatePowerUp()
    {
        transform.Rotate (new Vector3(0,Time.deltaTime * -100, 0));
    }

    /// <summary>
    /// boundary handling
    /// </summary>
    void CheckOutOfBounds()
    {
        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }
}
