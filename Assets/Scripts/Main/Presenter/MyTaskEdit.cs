using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTaskEdit : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button EditCompleteButton;

    private void Start()
    {
        EditCompleteButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.MyTaskCanvasGroup);
        });
    }
}
