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

    // 定数
    static string appClientId = AWSCognitoIDs.AppClientId;
    static string userPoolId = AWSCognitoIDs.UserPoolId;

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
            await CompleteSignin();
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
            userPoolId,
            appClientId,
            provider
        );
        CognitoUser user = new CognitoUser(
            emailField.text,
            appClientId,
            userPool,
            provider
        );

        AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
        {
            Password = passwordField.text
        }).ConfigureAwait(true);

        // for debug
        Debug.Log(user.SessionTokens.IdToken);
        GameManager.Instance.Session = user.SessionTokens;

        var userInfo =  await API<DecodeIdtoken.Response>.Get(DecodeIdtoken.FUNC_NAME);
    }

    /**
     * <summary>
     * サインインが正常に完了したときの処理
     * </summary>
     */
    public async UniTask CompleteSignin()
    {
        await Extensions.GetMyUser();

        await Extensions.TransitScene(SceneType.MAIN);
    }
}
