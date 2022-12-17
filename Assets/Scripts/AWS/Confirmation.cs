
using System;
using UnityEngine;
using TMPro;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

public class Confirmation : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField confirmationCodeField;
    static string appClientId = AWSCognitoIDs.AppClientId;

    public async void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        ConfirmSignUpRequest confirmSignUpRequest = new ConfirmSignUpRequest();

        confirmSignUpRequest.Username = emailField.text;
        confirmSignUpRequest.ConfirmationCode = confirmationCodeField.text;
        confirmSignUpRequest.ClientId = appClientId;

        try
        {
            ConfirmSignUpResponse confirmSignUpResult = await client.ConfirmSignUpAsync(confirmSignUpRequest);
            Debug.Log(confirmSignUpResult.ToString());
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}
