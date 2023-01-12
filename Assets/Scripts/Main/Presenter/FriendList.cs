using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class FriendList : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button addButton;
    [SerializeField] private RectTransform content;
    [SerializeField] private FriendPrefab friendPrefab;

    private List<FriendPrefab> friendPrefabs = new List<FriendPrefab>();
    public List<User> friends = new List<User>();

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
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.AccountCanvasGroup);
        });

        addButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.AddFriendCanvasGroup);
        });
    }

    private void Pooling()
    {
        for (int i = 0; i < GameManager.MAX_FRIEND_NUM; ++i)
        {
            var prefab = Instantiate(friendPrefab, content);
            friendPrefabs.Add(prefab);
            friendPrefabs[i].gameObject.SetActive(false);
        }
    }

    private async UniTask Setup()
    {
        await UniTask.WaitUntil(() => friendPrefabs.Count >= GameManager.MAX_FRIEND_NUM);

        var res = await Extensions.GetMyFriends();
        friends = res.friends.ConvertAll(x => new User(x));

        int i = 0;
        foreach (var friend in friends)
        {
            var userData = await Extensions.GetUser(friend);
            friend.Fetch(userData);

            friendPrefabs[i].Fetch(friend); 
            friendPrefabs[i].gameObject.SetActive(true);
            i++;
        }

        for (int j = i; j < GameManager.MAX_TASK_NUM; ++j)
        {
            friendPrefabs[j].gameObject.SetActive(false);
        }
    }
}
