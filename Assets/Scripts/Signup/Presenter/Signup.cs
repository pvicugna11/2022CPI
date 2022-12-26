using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.CognitoIdentityProvider.Model; // for SignUpRequest
using Cysharp.Threading.Tasks;

/**
 * <summary>
 * サインアップのPresenterクラス
 * </summary>
 */
public class Signup : MonoBehaviour
{
    [Header("View")]
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_InputField NicknameField;
    public Button SignupButton;

    // 定数
    static string appClientId = AWSCognitoIDs.AppClientId;

    private void Start()
    {
        SignupButton.onClick.AddListener(async () =>
        {
            if (await OnSignupClick())
            {
                CompleteSignup();
            }
        });
    }

    public async UniTask<bool> OnSignupClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        var sr = new SignUpRequest();
        string email = emailField.text;
        string password = passwordField.text;
        string nickname = NicknameField.text;

        sr.ClientId = appClientId;
        sr.Username = email;
        sr.Password = password;
        sr.UserAttributes = new List<AttributeType>
        {
            new AttributeType
            {
                Name = "email",
                Value = email,
            },
            new AttributeType
            {
                Name = "nickname",
                Value = nickname,
            }
        };

        try
        {
            SignUpResponse result = await client.SignUpAsync(sr);
            Debug.Log(result);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    public void CompleteSignup()
    {
        GameManager.Instance.Player = new MyUser()
        {
            nickname = NicknameField.text,
            email = emailField.text,
        };

        SignupUIManager.Instance.SetCanvasGroup(SignupUIManager.Instance.Confirmation);
    }
}
