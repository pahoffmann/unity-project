using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideVaccine : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MoveVaccine();
        DestroyVaccine();
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
        if (transform.position.y > 20f)
        {

            Destroy(this.gameObject);
        }
    }
    
    /*if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextVaccination)
    {
        _nextVaccination = Time.time + _vaccinationRate;
        
        Instantiate(_sideVaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                Quaternion.identity);
        
    }*/
    //muss eig in den Player rein?
}

