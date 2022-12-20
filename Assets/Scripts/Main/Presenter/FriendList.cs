using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendList : MonoBehaviour
{
    [Header("View")]
    [SerializeField] Button BackButton;
    [SerializeField] Button AddButton;

    private void Start()
    {
        BackButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.AccountCanvasGroup);
        });

        AddButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.AddFriendCanvasGroup);
        });
    }
}
