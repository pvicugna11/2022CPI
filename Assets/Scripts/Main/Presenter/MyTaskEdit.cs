using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class MyTaskEdit : MonoBehaviour
{
    private List<Task> tasks = new List<Task>();

    [Header("View")]
    [SerializeField] private Button editCompleteButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button deleteButton;
    [Space(10)]
    [SerializeField] private Transform content;
    [SerializeField] private TMP_InputField taskInputFieldPrefab;

    private List<TMP_InputField> taskInputFields = new List<TMP_InputField>();

    private const int MAX_TASK_NUM = 10;

    private void Awake()
    {
        for (int i = 0; i < MAX_TASK_NUM; ++i)
        {
            var comp = Instantiate(taskInputFieldPrefab, content);
            comp.onEndEdit.AddListener(str =>
            {
                tasks[i] = new Task(str);
            });
            taskInputFields.Add(comp);
            comp.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        tasks = GameManager.Instance.Tasks;

        int i = 0;
        foreach (var comp in tasks)
        {
            taskInputFields[i].gameObject.SetActive(true);
            taskInputFields[i].text = comp.name;

            i++;
        }
    }

    private void Start()
    {
        editCompleteButton.onClick.AddListener(async () =>
        {
            await OnEditCompleteClick();
            MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.MyTaskCanvasGroup);
        });

        addButton.onClick.AddListener(() =>
        {
            OnAddClick();
        });

        deleteButton.onClick.AddListener(() =>
        {
            OnDeleteClick();
        });
    }

    private async UniTask OnEditCompleteClick()
    {
        var postData = new CreateTask.PostData()
        {
            tasks = tasks,
        };
        await API<CreateTask.Response>.Post(CreateTask.FUNC_NAME, JsonUtility.ToJson(postData));
    }

    private void OnAddClick()
    {
        tasks.Add(new Task(""));

        taskInputFields[tasks.Count - 1].gameObject.SetActive(true);
        taskInputFields[tasks.Count - 1].text = "";
    }

    private void OnDeleteClick()
    {
        if (tasks.Count == 1) { return; }

        tasks.RemoveAt(tasks.Count - 1);

        taskInputFields[tasks.Count - 1].gameObject.SetActive(false);
        taskInputFields[tasks.Count - 1].text = "";
    }
}
