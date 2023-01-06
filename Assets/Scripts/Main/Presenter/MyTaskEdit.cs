using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class MyTaskEdit : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button editCompleteButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button deleteButton;
    [Space(10)]
    [SerializeField] private Transform content;
    [SerializeField] private TMP_InputField taskInputFieldPrefab;
    
    public List<TMP_InputField> taskInputFields = new List<TMP_InputField>();
    public int taskNum;

    private void Awake()
    {
        for (int i = 0; i < GameManager.MAX_TASK_NUM; ++i)
        {
            var comp = Instantiate(taskInputFieldPrefab, content);
            taskInputFields.Add(comp);
            comp.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        int i = 0;
        foreach (var comp in GameManager.Instance.Tasks)
        {
            taskInputFields[i].gameObject.SetActive(true);
            taskInputFields[i].text = comp.name;
            i++;
        }

        taskNum = i;
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
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < GameManager.MAX_TASK_NUM; ++i)
        {
            if (!taskInputFields[i].gameObject.activeSelf) { break; }

            tasks.Add(new Task(taskInputFields[i].text));
        }

        await Extensions.SetMyTasks(tasks);
    }

    private void OnAddClick()
    {
        // タスクが上限以上ならreturn
        if (taskNum >= GameManager.MAX_TASK_NUM) { return; }

        taskInputFields[taskNum].gameObject.SetActive(true);
        taskInputFields[taskNum].text = "";
        taskNum++;
    }

    private void OnDeleteClick()
    {
        // タスクが1個以下ならreturn
        if (taskNum <= 1) { return; }

        taskInputFields[taskNum - 1].gameObject.SetActive(false);
        taskInputFields[taskNum - 1].text = "";
        taskNum--;
    }
}
