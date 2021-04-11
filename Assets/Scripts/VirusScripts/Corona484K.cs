using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona484K : MonoBehaviour
{
    // schnellere Virusvariante
    [SerializeField] private float _speed = 6f;

    private int _score = 2;
    
    void Update()
    {
      transform.Translate(Vector3.down * _speed * Time.deltaTime);
      if (transform.position.y < -6f)
      {
          GameObject.FindObjectOfType<UIManager>().AddScore(-1);
          Destroy(this.gameObject);
      }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag(Constants.Tags.Vaccine))
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(_score);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag(Constants.Tags.UVLight))
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(_score);
            Destroy(this.gameObject);
        }
    }
}   

