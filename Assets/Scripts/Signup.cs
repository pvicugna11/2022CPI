using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.CognitoIdentityProvider.Model; // for SignUpRequest

public class Signup : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    static string appClientId = AWSCognitoIDs.AppClientId;

    public async void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        var sr = new SignUpRequest();
        string email = emailField.text;
        string password = passwordField.text;

        sr.ClientId = appClientId;
        sr.Username = email;
        sr.Password = password;
        sr.UserAttributes = new List<AttributeType> {
            new AttributeType {
                Name = "email",
                Value = email
            }
        };

        try
        {
            SignUpResponse result = await client.SignUpAsync(sr);
            Debug.Log(result);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}
