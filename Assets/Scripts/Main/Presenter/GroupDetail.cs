using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class GroupDetail : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI subTitle;
    [SerializeField] private RectTransform content;
    [SerializeField] private MemberPrefab memberPrefab;

    private List<MemberPrefab> memberPrefabs = new List<MemberPrefab>();

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
    }

    private void Pooling()
    {
        for (int i = 0; i < GameManager.MAX_TASK_NUM; ++i)
        {
            var prefab = Instantiate(memberPrefab, content);
            memberPrefabs.Add(prefab);
            memberPrefabs[i].gameObject.SetActive(false);
        }
    }

    private async UniTask Setup()
    {
        title.SetText(GameManager.Instance.CurrentGroup.name);
        subTitle.SetText($"開始: {GameManager.Instance.CurrentGroup.startDate.character}");

        await UniTask.WaitUntil(() => memberPrefabs.Count >= GameManager.MAX_GROUP_MEMBER_NUM);

        int i = 0;
        foreach (var member in GameManager.Instance.CurrentGroup.members)
        {
            await Extensions.GetUser(member);
            memberPrefabs[i].Fetch(member.name);
            memberPrefabs[i].m_Button.onClick.RemoveAllListeners();
            memberPrefabs[i].m_Button.onClick.AddListener(() =>
            {
                GameManager.Instance.CurrentUser = member;
                MainUIManager.Instance.SetCanvasGroup(MainUIManager.Instance.PersonalTaskCanvasGroup);
            });
            memberPrefabs[i].gameObject.SetActive(true);
            i++;
        }

        for (int j = i; j < GameManager.MAX_TASK_NUM; ++j)
        {
            memberPrefabs[j].gameObject.SetActive(false);
        }
    }
}
