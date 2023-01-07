using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class MyTask : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button editButton;
    [SerializeField] private Transform content;
    [SerializeField] private TaskPrefab taskPrefab;

    public List<TaskPrefab> taskPrefabs = new List<TaskPrefab>();

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
        editButton.onClick.AddListener(() =>
        {
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.MyTaskEditCanvasGroup);
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
        // 自分のタスクの取得
        await Extensions.GetUserTasks(GameManager.Instance.Player.id);

        // タスクがなかったら起床のタスクを作成
        if (GameManager.Instance.Tasks.Count == 0)
        {
            Debug.Log("タスクの生成");
            GameManager.Instance.Tasks.Add(new Task("起床"));
            await Extensions.SetMyTasks(GameManager.Instance.Tasks);
        }

        await UniTask.WaitUntil(() => taskPrefabs.Count >= GameManager.MAX_TASK_NUM);

        int i = 0;
        foreach (var task in GameManager.Instance.Tasks)
        {
            taskPrefabs[i].Fetch(task);
            taskPrefabs[i].gameObject.SetActive(true);
            i++;
        }
    }
}
