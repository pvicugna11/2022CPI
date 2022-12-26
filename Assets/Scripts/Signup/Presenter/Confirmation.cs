
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Cysharp.Threading.Tasks;

public class Confirmation : MonoBehaviour
{
    [Header("View")]
    public TMP_InputField confirmationCodeField;
    public Button ConfirmationButton;

    // 定数
    static string appClientId = AWSCognitoIDs.AppClientId;

    private void Start()
    {
        ConfirmationButton.onClick.AddListener(async () =>
        {
            if (await OnConfirmationClick())
            {
                await CompleteConfirmation();
            }
        });
    }

    public async UniTask<bool> OnConfirmationClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        ConfirmSignUpRequest confirmSignUpRequest = new ConfirmSignUpRequest();

        confirmSignUpRequest.Username = GameManager.Instance.Player.email;
        confirmSignUpRequest.ConfirmationCode = confirmationCodeField.text;
        confirmSignUpRequest.ClientId = appClientId;

        try
        {
            ConfirmSignUpResponse confirmSignUpResult = await client.ConfirmSignUpAsync(confirmSignUpRequest);
            Debug.Log(confirmSignUpResult.ToString());
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    public async UniTask CompleteConfirmation()
    {
        await Extensions.TransitScene(SceneType.SIGNIN);
    }
}
