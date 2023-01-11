using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    public TMP_InputField input;

    public void OnButtonClick()
    {
        Debug.Log(input.text);
        input.text = "";
    }
}
