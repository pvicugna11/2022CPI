using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager<T> : MonoBehaviour where T : UIManager<T>
{
    public static T Instance { get; protected set; }

    public CanvasGroup currentCanvasGroup { get; protected set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
    }

    public void SetCanvasGroup(CanvasGroup to)
    {
        HiddenCanvasGroup(currentCanvasGroup);
        DisplayCanvasGroup(to);
    }

    private void HiddenCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.gameObject.SetActive(false);
    }

    public void DisplayCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.gameObject.SetActive(true);

        currentCanvasGroup = canvasGroup;
    }
}
