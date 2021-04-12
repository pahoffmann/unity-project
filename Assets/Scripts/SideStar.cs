using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideStar : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coroner"))
        {
            Destroy(other.gameObject);
            GameObject.FindObjectOfType<UIManager>().AddScore(1);

        }
    }
   
}
