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
            friendSearch.IsFriendImage.color = GetImageColor(await Extensions.IsMyFriend(res.id));
            friendSearch.RequestFriendButton.onClick.RemoveAllListeners();
            friendSearch.RequestFriendButton.onClick.AddListener(async () =>
            {
                if (await Extensions.IsMyFriend(res.id))
                {
                    await Extensions.DeleteMyFriend(res.id);
                    friendSearch.IsFriendImage.color = GetImageColor(false);
                }
                else
                {
                    await Extensions.AddMyFriend(res.id);
                    friendSearch.IsFriendImage.color = GetImageColor(true);
                }
            });
            friendSearch.gameObject.SetActive(true);
        });
    }

    private Color GetImageColor(bool _isFriend)
    {
        return _isFriend ? new Color(0, 1, 0) : new Color(1, 0, 0);
    }
}
