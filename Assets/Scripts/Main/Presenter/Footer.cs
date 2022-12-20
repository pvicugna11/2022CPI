using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Footer : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button AccountButton;
    [SerializeField] private Button GroupButton;
    [SerializeField] private Button TaskButton;

    private void Start()
    {
        AccountButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.AccountCanvasGroup);
        });

        GroupButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.GroupListCanvasGroup);
        });

        TaskButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.MyTaskCanvasGroup);
        });
    }
}
