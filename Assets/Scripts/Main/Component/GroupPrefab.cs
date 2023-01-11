using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroupPrefab : MonoBehaviour
{
    public TextMeshProUGUI GroupName;
    public Button m_Button { get; private set; }

    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }

    public void Fetch(string groupName)
    {
        GroupName.SetText(groupName);
    }
}
