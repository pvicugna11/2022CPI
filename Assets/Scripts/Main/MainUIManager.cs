using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : UIManager<MainUIManager>
{
    [Header("Canvas Groups")]
    public CanvasGroup MyTaskCanvasGroup;
    public CanvasGroup MyTaskEditCanvasGroup;
    public CanvasGroup GroupListCanvasGroup;
    public CanvasGroup GroupDetailCanvasGroup;
    public CanvasGroup PersonalTaskCanvasGroup;
    public CanvasGroup AccountCanvasGroup;
    public CanvasGroup FriendListCanvasGroup;
    public CanvasGroup AddFriendCanvasGroup;
    public CanvasGroup CreateGroupCanvasGroup;

    private void Start()
    {
        DisplayCanvasGroup(MyTaskCanvasGroup);
    }
}
