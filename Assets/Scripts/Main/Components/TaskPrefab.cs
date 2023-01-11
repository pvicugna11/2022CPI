using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskPrefab : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public Image StatusImage;

    public void Fetch(Task task)
    {
        NameText.SetText(task.name);
        StatusImage.color = GetColor(task.isFinished);
    }

    public Color GetColor(bool isFinished)
    {
        return isFinished ? new Color(0, 1, 0) : new Color(0, 0, 0); 
    }
}
