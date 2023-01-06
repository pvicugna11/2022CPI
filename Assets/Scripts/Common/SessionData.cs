using System;
using Amazon.Extensions.CognitoAuthentication;

[Serializable]
public class SessionData
{
    public string IdToken;
    public string RefreshToken;

    public void Set(CognitoUserSession _session)
    {
        IdToken = _session.IdToken;
        RefreshToken = _session.RefreshToken;
    }
}
