using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaB1128 : MonoBehaviour
{
    //Virusvariante die etwas langsamer ist, aber dafür größer und mit 2 Leben
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _vlives = 3;
    [SerializeField] private Vector3 scaleChange;

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
}
