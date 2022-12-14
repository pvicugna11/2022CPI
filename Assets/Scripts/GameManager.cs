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
    public bool IsDebug;

    // ユーザ情報
    public SessionData Session = new SessionData();
    public MyUser Player;
    public Date LoginDate;
    public bool IsLogin { get; set; }

    // タスク
    public List<Task> Tasks = new List<Task>();

    // グループ
    public Group CurrentGroup;
    public User CurrentUser;

    // 定数
    public static string appClientId = AWSCognitoIDs.AppClientId;
    public static string userPoolId = AWSCognitoIDs.UserPoolId;
    public const int MAX_TASK_NUM = 10;         // タスクの最大数
    public const int MAX_GROUP_NUM = 10;        // グループの最大数
    public const int MAX_GROUP_MEMBER_NUM = 10; // グループに所属するメンバーの最大数
    public const int MAX_FRIEND_NUM = 50;       // フレンドの最大数

    private async void Start()
    {
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

        public void Set()
        {
            GameManager.Instance.Session.RefreshToken = refreshToken;
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

            data.Set();

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

        user.SessionTokens = new CognitoUserSession("", "", Session.RefreshToken, DateTime.Now, DateTime.Now.AddHours(1));

        try
        {
            AuthFlowResponse context = await user.StartWithRefreshTokenAuthAsync(new InitiateRefreshTokenAuthRequest()
            {
                AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH,
            });

            Session.Set(user.SessionTokens);
            IsLogin = true;

            Debug.Log(Session.IdToken);
            
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

        if (IsDebug)
        {
            await Extensions.TransitScene(SceneType.DEBUG);
            return;
        }
        await Extensions.TransitScene(SceneType.MAIN);
    }
}
