using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddFriend : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_InputField idSearchInput;
    [SerializeField] private FriendSearch friendSearch;

    private void OnEnable()
    {
        idSearchInput.text = "";
        friendSearch.gameObject.SetActive(false);
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.FriendListCanvasGroup);
        });

        idSearchInput.onEndEdit.AddListener(async value =>
        {
            var res = await Extensions.GetUser(value);
            if (String.IsNullOrWhiteSpace(res.id))
            {
                friendSearch.gameObject.SetActive(false);
                return;
            }
            
            friendSearch.Nickname.SetText(res.nickname);
            friendSearch.gameObject.SetActive(true);
        });
    }
}
