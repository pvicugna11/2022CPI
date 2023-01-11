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

    private async UniTask Setup()
    {
        await Extensions.GetMyGroups();
    }
}
