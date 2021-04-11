using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    [SerializeField] private float _speed = 0f;

    private float _acceleration = 8;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 1000)
        {
            Destroy(gameObject);
        }
        _speed += _acceleration * Time.deltaTime;
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0, 0, 1);
        pos += localVectorUp * _speed * Time.deltaTime;
        transform.position = pos;
    }
}
