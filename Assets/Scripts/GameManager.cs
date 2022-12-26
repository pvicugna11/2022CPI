using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.CognitoIdentityProvider;
using Amazon;
using Cysharp.Threading.Tasks;

/**
 * <summary>
 * ゲームのすべての管理をするクラス
 * </summary>
 */
public sealed class GameManager : Singleton<GameManager>
{
    // ユーザ情報
    public CognitoUserSession Session { get; set; }
    public MyUser Player { get; set; }
    public Date LoginDate { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();

    // 定数
    public static string appClientId = AWSCognitoIDs.AppClientId;
    public static string userPoolId = AWSCognitoIDs.UserPoolId;

    protected override async void Awake()
    {
        base.Awake();

        if (!LoadSession()) { return; }

        await UniTask.WaitUntil(() => Player != null);

        if (await RefreshTokenSignin()) { await CompleteSignin(); }
    }

    [Serializable]
    class SaveData
    {
        public SaveData(string _refreshToken, MyUser _player, Date _date)
        {
            refreshToken = _refreshToken;
            player = _player;
            date = _date;
        }

        public string refreshToken;
        public MyUser player;
        public Date date;

        public void Fetch()
        {
            GameManager.Instance.Session = new CognitoUserSession("", "", refreshToken, DateTime.Now, DateTime.Now.AddHours(1));
            GameManager.Instance.Player = player;
            GameManager.Instance.LoginDate = new Date(DateTime.Now);
        }
    }

    public void SaveSession()
    {
        SaveData data = new SaveData(Session.RefreshToken, Player, new Date(DateTime.Now));
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public bool LoadSession()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            data.Fetch();

            return true;
        }
        else
        {
            return false;
        }
    }

    public async UniTask<bool> RefreshTokenSignin()
    {
        var provider = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.APNortheast1);
        CognitoUserPool userPool = new CognitoUserPool(
            userPoolId,
            appClientId,
            provider
        );

        CognitoUser user = new CognitoUser(
            Player.email,
            GameManager.appClientId,
            userPool,
            provider
        );

        user.SessionTokens = Session;

        try
        {
            AuthFlowResponse context = await user.StartWithRefreshTokenAuthAsync(new InitiateRefreshTokenAuthRequest()
            {
                AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH,
            });

            Session = user.SessionTokens;

            return true;
        }
        catch
        {
            return false;
        }
    }

    /**
     * <summary>
     * サインインが正常に完了したときの処理
     * </summary>
     */
    public async UniTask CompleteSignin()
    {
        await Extensions.GetMyUser();

        SaveSession();

        await Extensions.TransitScene(SceneType.MAIN);
    }
}
