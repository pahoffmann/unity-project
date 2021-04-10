using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{

    private string name;
    public GameObject inputField;
    public GameObject displayText;

    public void readTextInput()
    {
        name = inputField.GetComponent<Text>().text;
        Debug.Log(name);
        displayText.GetComponent<Text>().text = "Your name is: " + name;
    }

}
