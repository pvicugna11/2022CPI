using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.Extensions.CognitoAuthentication; // for CognitoUserPool
using Amazon; // for RegionEndpoint
using Cysharp.Threading.Tasks;

/**
 * <summary>
 * サインインのPresenterクラス
 * </summary>
 */
public class Signin : MonoBehaviour
{
    [Header("View")]
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Button SigninButton;
    public Button SignupButton;

    private void Start()
    {
        SigninButton.onClick.AddListener(async () =>
        {
            await OnSigninClick();
        });

        SignupButton.onClick.AddListener(async () =>
        {
            await Extensions.TransitScene(SceneType.SIGNUP);
        });
    }

    public async UniTask OnSigninClick()
    {
        try
        {
            await AuthenticateWithSrpAsync();
            await GameManager.Instance.CompleteSignin();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public async UniTask AuthenticateWithSrpAsync()
    {
        var provider = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.APNortheast1);
        CognitoUserPool userPool = new CognitoUserPool(
            GameManager.userPoolId,
            GameManager.appClientId,
            provider
        );
        CognitoUser user = new CognitoUser(
            emailField.text,
            GameManager.appClientId,
            userPool,
            provider
        );

        AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
        {
            Password = passwordField.text
        }).ConfigureAwait(true);

        GameManager.Instance.Session = user.SessionTokens;

        // for debug
        Debug.Log(GameManager.Instance.Session.IdToken);
    }
}
