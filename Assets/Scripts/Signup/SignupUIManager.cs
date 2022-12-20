using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignupUIManager : UIManager<SignupUIManager>
{    
    public CanvasGroup Signup;
    public CanvasGroup Confirmation;

    private void Start()
    {
        DisplayCanvasGroup(Signup);
    }
}
