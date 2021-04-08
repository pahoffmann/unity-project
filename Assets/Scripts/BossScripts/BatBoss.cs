using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatBoss : MonoBehaviour
{
    // Start is called before the first frame update

    private bool _inPosition = false;
    private float _speed = 4f;
    private bool _finishedRotating = true;
    private int _maxClones = 4;
    public int _lives { get; set; }
    void Start()
    {
        transform.Rotate(new Vector3(30, 180, 0), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Constants.Dimensions.BorderTop - 2 && !_inPosition)
        {
            transform.Translate(0,-1 * Time.deltaTime,0, Space.World);
        }
        else
        {
            if (_finishedRotating)
            {
                int rand1 = Random.Range(0, 1000);
                int rand2 = Random.Range(0, 1000);

                if (rand1 == rand2)
                {
                    _finishedRotating = false;
                    StartCoroutine(RotationCoroutine());
                }
            }
            //do some random movement
        }
    }

    IEnumerator RotationCoroutine()
    {
        Debug.Log("Started new Coroutine");
        int counter = 0;
        
        while (!_finishedRotating)
        {
            transform.Rotate(new Vector3(0,1,0), Space.Self);
            counter++;
            yield return new WaitForSeconds(0.01f);
            
            if (counter == 360)
            {
                Debug.Log("Finished Coroutine");
                _finishedRotating = true;
            }
            // we want to clone the boss, maybe give the clones some opacity, but only if this objects parent is actually the wavemanager
            else if (counter % 60 == 0 && transform.parent.childCount < 4)
            {
                GameObject clone = Instantiate(gameObject, 
                    new Vector3(Random.Range(Constants.Dimensions.BorderLeft,Constants.Dimensions.BorderRight), transform.position.y, 0), 
                    Quaternion.identity) 
                    as GameObject;
                clone.GetComponent<BatBoss>()._lives = 1;
                //TODO: scaling only for clones of the original
                clone.transform.localScale = new Vector3(clone.transform.localScale.x * 0.5f, clone.transform.localScale.y * 0.5f, clone.transform.localScale.z * 0.5f);
                clone.transform.SetParent(transform.parent);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            other.GetComponent<Player>().Damage();
            Destroy(gameObject);
        }
        else if (other.CompareTag(Constants.Tags.Vaccine))
        {
            Destroy(other.gameObject);
            _lives -= 1;
            if (_lives == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
