using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void SetCanvasGroup(CanvasGroup from, CanvasGroup to)
    {
        from.alpha = 0;
        from.interactable = false;
        from.blocksRaycasts = false;

        to.alpha = 1;
        to.interactable = true;
        to.blocksRaycasts = true;
    }
}
