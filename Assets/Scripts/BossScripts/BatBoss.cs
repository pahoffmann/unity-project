using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject _baseShot;
    
    
    private bool _inPosition = false;
    private float _speed = 4f;
    private bool _finishedRotating = true;
    private int _maxClones = 4;
    private float _nextRandomMove;
    private float _timeBetweenRands = 2f;
    private float _randX = 0f;
    private float _randY = 0f;
    public int _lives { get; set; }
    void Start()
    {
        transform.Rotate(new Vector3(30, 180, 0), Space.Self);
        _nextRandomMove = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        // in the beginning, move down
        if (transform.position.y > Constants.Dimensions.BorderTop - 2 && !_inPosition)
        {
            transform.Translate(0,-1 * Time.deltaTime,0, Space.World);
        }
        else
        {
            //random movement
            // TODO: implement abstoßung vong wand her.
            
            if (Time.time > _nextRandomMove)
            {
                _nextRandomMove = Time.time + _timeBetweenRands;
                _randX = Random.Range(-4, 4);
                _randY = Random.Range(-4, 4);

                if (_randX < 0 && _randX > -2)
                {
                    _randX = -2;
                }
                else if(_randX >= 0 && _randX < 2)
                {
                    _randX = 2;
                }
                
                if (_randY < 0 && _randY > -2)
                {
                    _randY -= 1;
                }
                else if (_randY >= 0 && _randY < 2)
                {
                    _randY += 1;
                }

                Instantiate(_baseShot, transform.position, Quaternion.identity);
            }
        
            transform.Translate(_randX * Time.deltaTime, _randY * Time.deltaTime, 0, Space.World);
            
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

            CheckBoundaries();
            //do some random movement
        }
        
        // random movement 
    }

    void CheckBoundaries()
    {
        float floorOffset = 4;
        
        float ceiling   = Constants.Dimensions.BorderTop, 
            floor     = Constants.Dimensions.BorderBottom,
            leftWall  = Constants.Dimensions.BorderLeft,
            rightWall = Constants.Dimensions.BorderRight;
        
        // player boundaries
        // top, bottom
        if (transform.position.y > ceiling - 1)
        {
            transform.position = new Vector3(transform.position.x, ceiling - 1, 0);
        } 
        else if (transform.position.y < floor + floorOffset)
        {
            transform.position = new Vector3(transform.position.x, floor + floorOffset, 0);
        }

        // left, right
        if (transform.position.x > rightWall - 1)
        {
            transform.position = new Vector3(rightWall - 1, transform.position.y, 0);
        } 
        else if (transform.position.x < leftWall + 1)
        {
            transform.position = new Vector3(leftWall + 1, transform.position.y, 0);
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
            else if (counter % 60 == 0 && transform.parent.childCount < 4 && transform.parent.GetChild(0) == transform)
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
