using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Account : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private TextMeshProUGUI id;
    [SerializeField] private TextMeshProUGUI email;
    [SerializeField] private Button friendListButton;

    private void Awake()
    {
        friendListButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.FriendListCanvasGroup);
        });
    }

    private void OnEnable()
    {
        userName.SetText(GameManager.Instance.Player.nickname);
        id.SetText(GameManager.Instance.Player.id);
        email.SetText(GameManager.Instance.Player.email);
    }
}
