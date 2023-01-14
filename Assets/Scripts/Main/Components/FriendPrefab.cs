using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendPrefab : MonoBehaviour
{
    public TextMeshProUGUI UserName;

    public virtual void Fetch(User user)
    {
        UserName.SetText(user.name);
    }
}
