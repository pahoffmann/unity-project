using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vaccine : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (this.CompareTag("UVLight"))
            speed = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateVaccine();
        MoveVaccine();
        DestroyVaccine();
    }

    /// <summary>
    /// rotation
    /// </summary>
    void RotateVaccine()
    {
        if (this.CompareTag("Vaccine"))
            transform.Rotate (new Vector3(0,Time.deltaTime * 200, 0));
    }
    
    /// <summary>
    /// movement
    /// </summary>
    void MoveVaccine()
    {
        // moves the vaccine up with a given speed
        transform.Translate(0f, speed * Time.deltaTime, 0f);
    }

    /// <summary>
    /// Destroys vaccine if out of bounds
    /// </summary>
    void DestroyVaccine()
    {
        // destroys the vaccine once it reaches a certain height
        if (transform.position.y > 6f)
        {
            Destroy(this.gameObject);
        }
    }
}
