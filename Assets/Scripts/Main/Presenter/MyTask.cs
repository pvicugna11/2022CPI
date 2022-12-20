using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTask : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button EditButton;

    private void Start()
    {
        EditButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.MyTaskEditCanvasGroup);
        });
    }
}
