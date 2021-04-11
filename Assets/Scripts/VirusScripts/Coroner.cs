using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coroner : MonoBehaviour
{
    
    [SerializeField] private float _coronaSpeed = 3f;
    [SerializeField] private float _fasterCoronaDuration = 7f;
    [SerializeField] private float _slowerCoronaDuration = 7f;

    [SerializeField] private float _b117DirectionalVariance = 30f;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCorona();
        MoveCorona();
    }

    /// <summary>
    /// rotation
    /// </summary>
    void RotateCorona()
    {
        // transform.Rotate (new Vector3(0,Time.deltaTime * -100, 0), Space.Self);
        transform.Rotate (new Vector3(0f, Time.deltaTime * -100, 0), Space.Self);
    } 
    
    /// <summary>
    /// movement
    /// </summary>
    void MoveCorona()
    {
        if (name.Contains("B117"))
            transform.Translate(_b117DirectionalVariance * Random.Range(-1f, 1f) * Time.deltaTime, -_coronaSpeed * Time.deltaTime, 0f, Space.World);
        else 
            transform.Translate(0f, -_coronaSpeed * Time.deltaTime, 0f, Space.World);
        // Destroy Corona if out of camera frustum 
        if (transform.position.y < -6f)
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(-1);
            Destroy(this.gameObject);
        }
    }
    
    /// <summary>
    /// collision handling
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // collision handling
        int score = 1;
        if (name.Contains("B117"))
            score = 3;
        
        if (other.CompareTag(Constants.Tags.Vaccine))
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(score);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        } else if (other.CompareTag(Constants.Tags.UVLight)) 
        {
            GameObject.FindObjectOfType<UIManager>().AddScore(score);
            Destroy(this.gameObject);
        } else if (other.CompareTag(Constants.Tags.Player))
        {
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
    }

    public void FasterCorona()
    {
        StartCoroutine(FasterCoronaCoroutine(_fasterCoronaDuration));
    }

    IEnumerator FasterCoronaCoroutine(float duration)
    {
        _coronaSpeed = 7f;
        yield return new WaitForSeconds(duration);
        _coronaSpeed = 3f;
    }
    

    public void SlowerCorona()
    {
        StartCoroutine(SlowerCoronaCoroutine(_slowerCoronaDuration));
    }
    
    IEnumerator SlowerCoronaCoroutine(float duration)
    {
        _coronaSpeed = 1f;
        yield return new WaitForSeconds(duration);
        _coronaSpeed = 3f;
    }
    
}
