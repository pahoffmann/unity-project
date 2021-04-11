using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaY144 : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 3f;

    [SerializeField] 
    private GameObject _evilvaccineprefab;

    [SerializeField] 
    private float _incidentRate = 2f;

    private float _canInfect = -1f;

    private int _score = 2;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(-1);
            Destroy(this.gameObject);
        }
        Infect();
    }
    public void Infect()
    {
        if (Time.time > _canInfect)
        {
            _canInfect = Time.time + _incidentRate;
            Instantiate(_evilvaccineprefab, transform.position + new Vector3(0f, -0.7f, 0f), Quaternion.identity);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //if the object we collide with is the player
        if (other.CompareTag("Player"))
        {
            //damage player or destroy it
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        
        //but if the other one is vaccine
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
