using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class PersonalTask : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI subTitle;
    [SerializeField] private RectTransform content;
    [SerializeField] private TaskPrefab taskPrefab;

    private List<TaskPrefab> taskPrefabs = new List<TaskPrefab>();
    private List<Task> tasks = new List<Task>();

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
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.GroupDetailCanvasGroup);
        });
    }

    private void Pooling()
    {
        for (int i = 0; i < GameManager.MAX_TASK_NUM; ++i)
        {
            var prefab = Instantiate(taskPrefab, content);
            taskPrefabs.Add(prefab);
            taskPrefabs[i].gameObject.SetActive(false);
        }
    }

    private async UniTask Setup()
    {
        title.SetText($"{GameManager.Instance.CurrentUser.name}のタスク");

        tasks = await Extensions.GetUserTasks(GameManager.Instance.CurrentUser.id);

        await UniTask.WaitUntil(() => taskPrefabs.Count >= GameManager.MAX_TASK_NUM);

        int i = 0;
        foreach (var task in tasks)
        {
            taskPrefabs[i].Fetch(task);
            taskPrefabs[i].gameObject.SetActive(true);
            i++;
        }

        for (int j = i; j < GameManager.MAX_TASK_NUM; ++j)
        {
            taskPrefabs[j].gameObject.SetActive(false);
        }
    }
}
