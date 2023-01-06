using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    public TMP_InputField input;

    public async void GetMyTask()
    {
        await Extensions.GetUserTasks(input.text);
    }
}
