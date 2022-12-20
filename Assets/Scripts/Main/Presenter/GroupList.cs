using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupList : MonoBehaviour
{
    [Header("View")]
    [SerializeField] Button CreateButton;

    private void Start()
    {
        CreateButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.CreateGroupCanvasGroup);
        });
    }
}
