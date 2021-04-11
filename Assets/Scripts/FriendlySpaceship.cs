using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpaceship : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        if (transform.position.y < -6f)
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(1);
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
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("UVLight"))
        {
            Destroy(this.gameObject);
        }
    }
}
