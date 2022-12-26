using System.Collections.Generic;
using UnityEngine;
using Amazon.Extensions.CognitoAuthentication;

/**
 * <summary>
 * ゲームのすべての管理をするクラス
 * </summary>
 */
public sealed class GameManager : Singleton<GameManager>
{
    // ユーザ情報
    public CognitoUserSession Session { get; set; }
    public MyUser myUser { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();

    protected override async void Awake()
    {
        base.Awake();

        if (Extensions.IsSceneName(SceneType.MAIN))
        {
            if (Session == null) { return; }

            await Extensions.GetMyUser();
            Debug.Log($"{myUser.id}, {myUser.nickname}, {myUser.email}");
        }
    }
}
