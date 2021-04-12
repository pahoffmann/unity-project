using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPowerUp : MonoBehaviour
{

    private float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1f * _speed * Time.deltaTime, 0), Space.World);
        transform.Rotate(new Vector3(0, 0, 20f * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            other.GetComponent<Player>().UpgradeShot();
            Destroy(gameObject);
        }
    }
}
