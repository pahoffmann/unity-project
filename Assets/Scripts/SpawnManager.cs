using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    // Corona shit
    [SerializeField] private GameObject _coronaPrefab;

    [SerializeField] private GameObject _b117Prefab;
    [SerializeField] private float _coronaWaitTime = 1f;
    [SerializeField] private float _b117Bias = 2f;
    private IEnumerator _spawnVirusCoroutine;
    private bool _spawnVirusEnable = true;
     

    // PowerUp shit
    [SerializeField] private GameObject _powerUpPrefab;
    [SerializeField] private float _powerUpWaitTime = 20f;
    private IEnumerator _spawnPowerUpCoroutine;
    private bool _spawnPowerUpEnable = true;
    
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        StartCoroutine(SpawnVirusCoroutine());
        StartCoroutine(SpawnPowerUpCoroutine());
        StartCoroutine(AdjustB117Bias());
    }

    // Update is called once per frame
    void Update()
    {
        if (_b117Bias < 100)
        {
            StopCoroutine(AdjustB117Bias());
        }
    }

    /// <summary>
    /// deactivates spawning of viruses if player dead 
    /// </summary>
    public void onPlayerDeath()
    {
        _spawnVirusEnable = false;
    }

    /// <summary>
    /// Virus spawning coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnVirusCoroutine()
    {
        while (_spawnVirusEnable)
        {
            // adjust b117 spawn rate by a bias to model exponential growth
            float whichVirus = Random.Range(-0.1f * _b117Bias, 1f);
            
            if (whichVirus >= 0f)
                Instantiate(_coronaPrefab, new Vector3(Random.Range(-5f, 5f), 8f, 0f), 
                    Quaternion.identity, this.transform);
            else
                Instantiate(_b117Prefab, new Vector3(Random.Range(-5f, 5f), 8f, 0f), 
                    Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_coronaWaitTime);
        }
    }

    /// <summary>
    /// PowerUp spawning coroutine
    /// </summary>
    /// <returns></returns>
     IEnumerator SpawnPowerUpCoroutine ()
    {
        while (_spawnPowerUpEnable)
        {
            yield return new WaitForSeconds(_powerUpWaitTime);
            Instantiate(_powerUpPrefab, new Vector3(Random.Range(-5f, 5f), 8f, 0f),
                Quaternion.identity, this.transform);
        }
    }

    IEnumerator AdjustB117Bias()
    {
        while (_spawnVirusEnable)
        {
            yield return new WaitForSeconds(10f);
            _b117Bias += 2f;
        }
    }
}
