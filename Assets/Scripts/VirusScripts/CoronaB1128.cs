using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaB1128 : MonoBehaviour
{
    //Virusvariante die etwas langsamer ist, aber dafür größer und mit 2 Leben
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _vlives = 3;
    [SerializeField] private Vector3 scaleChange;
    [SerializeField] private GameObject _coin;
    private float _coinDropProbabilty = 0.2f;

    private int _score = 3;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(-1);
            Destroy(this.gameObject);
        }
    }

    public void Damage()
    {
        _vlives -= 1;
        scaleChange = new Vector3(-0.03f, -0.03f, -0.03f);

        if (_vlives == 0)
        {
            Destroy(this.gameObject);
            //but if the other one is vaccine
            
            GameObject.FindObjectOfType<UIManager>().AddScore(_score);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Vaccine"))
        {
            Damage();
            transform.localScale += scaleChange;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("UVLight"))
        {
            Damage();
            transform.localScale += scaleChange;
        }
    }
    
    private void OnDestroy()
    {
        // spawn coin with probabilty
        int rand = Random.Range(0, 9);

        if (rand < _coinDropProbabilty * 10)
        {
            Instantiate(_coin, transform.position, Quaternion.Euler(90, 180, 0), transform.parent);
        }
    }
}
