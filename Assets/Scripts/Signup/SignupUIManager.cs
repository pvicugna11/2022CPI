using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignupUIManager : UIManager
{
    public static SignupUIManager Instance { get; private set; }
    
    public CanvasGroup Signup;
    public CanvasGroup Confirmation;

    private void Awake()
    {
        Instance = this;
    }
}
