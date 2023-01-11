using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class GroupList : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button createButton;
    [SerializeField] private Transform content;
    [SerializeField] private GroupPrefab groupPrefab;

    private List<GroupPrefab> groupPrefabs = new List<GroupPrefab>();

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
        createButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.CreateGroupCanvasGroup);
        });
    }

    private void Pooling()
    {
        for (int i = 0; i < GameManager.MAX_GROUP_NUM; ++i)
        {
            var prefab = Instantiate(groupPrefab, content);
            groupPrefabs.Add(prefab);
            groupPrefabs[i].gameObject.SetActive(false);
        }
    }

    private async UniTask Setup()
    {
        await UniTask.WaitUntil(() => groupPrefabs.Count >= GameManager.MAX_GROUP_NUM);

        createButton.gameObject.SetActive(GameManager.Instance.Player.groupNames.Count == GameManager.MAX_GROUP_NUM);

        int i = 0;
        foreach (var group in GameManager.Instance.Player.groupNames)
        {
            groupPrefabs[i].Fetch(group);
            groupPrefabs[i].m_Button.onClick.RemoveAllListeners();
            groupPrefabs[i].m_Button.onClick.AddListener(async () =>
            {
                await Extensions.GetMyGroup(group);
                MainUIManager.Instance.DisplayCanvasGroup(MainUIManager.Instance.GroupDetailCanvasGroup);
            });
            groupPrefabs[i].gameObject.SetActive(true);
            i++;
        }

        for (int j = i; j < GameManager.MAX_GROUP_NUM; ++j)
        {
            groupPrefabs[j].gameObject.SetActive(false);
        }
    }
}
