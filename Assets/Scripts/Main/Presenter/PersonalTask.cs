using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalTask : MonoBehaviour
{
    [Header("View")]
    [SerializeField] Button BackButton;

    private void Start()
    {
        BackButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.GroupDetailCanvasGroup);
        });
    }
}
