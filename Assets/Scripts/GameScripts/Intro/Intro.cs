using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private AudioClip _intro;
    
    private float _speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayIntroSound());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 141)
        {
            Destroy(gameObject);
        }
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
        pos += localVectorUp * _speed * Time.deltaTime;
        transform.position = pos;
    }
    
    IEnumerator PlayIntroSound()
    {
        AudioSource.PlayClipAtPoint(_intro, new Vector3(0,0,0));
        yield return new WaitForSeconds(0f);
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("Menu");
    }
}
