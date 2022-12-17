
using System;
using UnityEngine;
using TMPro;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.Extensions.CognitoAuthentication; // for CognitoUserPool
using Amazon; // for RegionEndpoint

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

    // 定数
    static string appClientId = AWSCognitoIDs.AppClientId;
    static string userPoolId = AWSCognitoIDs.UserPoolId;

    public void OnClick()
    {
        try
        {
            AuthenticateWithSrpAsync();
            CompleteSignin();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public async void AuthenticateWithSrpAsync()
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
    }

    /**
     * <summary>
     * サインインが正常に完了したときの処理
     * </summary>
     */
    public void CompleteSignin()
    {

    }
}
