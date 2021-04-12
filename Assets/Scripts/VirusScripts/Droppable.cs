using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Droppable : MonoBehaviour
{
    private float accY_;
    private float baseVelX;
    private float baseVelY = 6;
    // Start is called before the first frame update
    void Start()
    {
        baseVelX = Random.Range(-4, 4);
        accY_ = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        baseVelY += accY_ * Time.deltaTime; // accelerate
        Vector3 translation = new Vector3(baseVelX * Time.deltaTime, baseVelY * Time.deltaTime, 0);
        
        transform.Translate(translation, Space.World);

        if (transform.position.y < Constants.Dimensions.BorderBottom - 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(10);
            Destroy(gameObject);
        }
    }
}
