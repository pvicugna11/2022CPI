using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

namespace Main
{

public class CreateGroup : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_InputField groupInput;
    [SerializeField] private Button createGroupButton;
    [SerializeField] private RectTransform content;
    [SerializeField] private FriendGroupPrefab friendGroupPrefab;

    private List<FriendGroupPrefab> friendGroupPrefabs = new List<FriendGroupPrefab>();
    private List<User> friends = new List<User>();
    private Group group = new Group();

    private void Awake()
    {
        Pooling();
    }

    private async void OnEnable()
    {
        await Setup();
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.GroupListCanvasGroup);
        });

        groupInput.onEndEdit.AddListener(value =>
        {
            group.name = value;
        });

        createGroupButton.onClick.AddListener(async () =>
        {
            await Extensions.CreateMyGroup(group);
        });
    }

    private void Pooling()
    {
        for (int i = 0; i < GameManager.MAX_FRIEND_NUM; ++i)
        {
            var prefab = Instantiate(friendGroupPrefab, content);
            friendGroupPrefabs.Add(prefab);
            friendGroupPrefabs[i].gameObject.SetActive(false);
        }
    }

    private async UniTask Setup()
    {
        await UniTask.WaitUntil(() => friendGroupPrefabs.Count >= GameManager.MAX_FRIEND_NUM);

        group.members.Add(new User()
        {
            id = GameManager.Instance.Player.id,
            name = GameManager.Instance.Player.nickname,
        });
        
        var res = await Extensions.GetMyFriends();
        friends = res.friends.ConvertAll(x => new User(x));

        int i = 0;
        foreach (var friend in friends)
        {
            var userData = await Extensions.GetUser(friend);
            friend.Fetch(userData);

            friendGroupPrefabs[i].Fetch(friend);
            friendGroupPrefabs[i].AddFriendGroupToggle.onValueChanged.RemoveAllListeners();
            friendGroupPrefabs[i].AddFriendGroupToggle.onValueChanged.AddListener(isMember =>
            {
                if (isMember)
                {
                    group.members.Add(friend);
                }
                else
                {
                    group.members.Remove(friend);
                }
            });
            friendGroupPrefabs[i].gameObject.SetActive(true);
            i++;
        }

        for (int j = i; j < GameManager.MAX_TASK_NUM; ++j)
        {
            friendGroupPrefabs[j].gameObject.SetActive(false);
        }
    }
}
}
