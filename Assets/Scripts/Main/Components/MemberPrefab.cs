using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemberPrefab : MonoBehaviour
{
    public TextMeshProUGUI MemberName;
    public Button m_Button { get; private set; }

    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }

    public void Fetch(string name)
    {
        MemberName.SetText(name);
    }
}
