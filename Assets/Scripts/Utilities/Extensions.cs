using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/**
 * <summary>
 * 拡張クラス
 * </summary>
 */
public static class Extensions
{
    // ------------------------------ 共通 ------------------------------
    /**
     * <summary>
     * シーンを遷移する
     * <param name="type">シーンのタイプ</param>
     * </summary>
     */
    public static async UniTask TransitScene(SceneType type)
    {
        await SceneManager.LoadSceneAsync((int)type);
    }

    /**
     * <summary>
     * 現在のシーンと引数のシーンが一致しているか
     * <param name="type">シーンのタイプ</param>
     * </summary>
     */
    public static bool IsSceneName(SceneType type)
    {
        return SceneManager.GetActiveScene().handle == (int)type;
    }

    // ------------------------------ API ------------------------------
    // ------------------------------ USER ------------------------------
    /**
     * <summary>
     * 自分のユーザ情報を取得する
     * </summary>
     */
    public static async UniTask GetMyUser()
    {
        if (GameManager.Instance.Player == null) { GameManager.Instance.Player = new MyUser(); }

        GameManager.Instance.Player.SetMyUser(await API<DecodeIdtoken.Response>.Get(DecodeIdtoken.FUNC_NAME));
    }

    /**
     * <summary>
     * ユーザの情報を取得する
     * </summary>
     */
    public static async UniTask<GetUserData.Response> GetUser(string _id)
    {
        var postData = new GetUserData.PostData()
        {
            id = _id,
        };

        return await API<GetUserData.Response>.Post(GetUserData.FUNC_NAME, JsonUtility.ToJson(postData));
    }

    /**
     * <summary>
     * ユーザの情報を取得する
     * </summary>
     */
    public static async UniTask<GetUserData.Response> GetUser(User user)
    {
        var postData = new GetUserData.PostData()
        {
            id = user.id,
        };

        return await API<GetUserData.Response>.Post(GetUserData.FUNC_NAME, JsonUtility.ToJson(postData));
    }

    // ------------------------------ TASK ------------------------------
    /**
     * <summary>
     * タスク情報を取得する
     * </summary>
     */
    public static async UniTask<List<Task>> GetUserTasks(string _id)
    {
        var postData = new GetTasks.PostData()
        {
            id = _id,
        };

        var res = await API<GetTasks.Response>.Post(GetTasks.FUNC_NAME, JsonUtility.ToJson(postData));
        return res.tasks;
    }

    /**
     * <summary>
     * 自分のタスクを登録する
     * </summary>
     */
    public static async UniTask SetMyTasks(List<Task> tasks)
    {
        var postData = new CreateTask.PostData()
        {
            tasks = tasks,
        };

        await API<CreateTask.Response>.Post(CreateTask.FUNC_NAME, JsonUtility.ToJson(postData));
    }

    // ------------------------------ GROUP ------------------------------
    /**
     * <summary>
     * 自分の所属しているグループのデータを取得する
     * </summary>
     */
    public static async UniTask GetMyGroup(string groupName)
    {
        var postData = new GetGroup.PostData()
        {
            name = groupName,
        };

        GameManager.Instance.CurrentGroup.Set(groupName, await API<GetGroup.Response>.Post(GetGroup.FUNC_NAME, JsonUtility.ToJson(postData)));
    }

    /**
     * <summary>
     * グループ作成
     * </summary>
     */
    public static async UniTask CreateMyGroup(Group group)
    {
        var postData = new CreateGroup.PostData()
        {
            name = group.name,
            members = group.members.ConvertAll(x => x.id),
            startDate = new DateStr(group.startDate),
        };

        await API<CreateGroup.Response>.Post(CreateGroup.FUNC_NAME, JsonUtility.ToJson(postData));
    }

    // ------------------------------ FRIEND ------------------------------
    /**
     * <summary>
     * 自分のフレンドを取得する
     * </summary>
     */
    public static async UniTask<GetFriends.Response> GetMyFriends()
    {
        var postData = new GetFriends.PostData()
        {
            client =  GameManager.Instance.Player.id,
        };

        return await API<GetFriends.Response>.Post(GetFriends.FUNC_NAME, JsonUtility.ToJson(postData));
    }

    /**
     * <summary>
     * 自分とフレンドかどうか
     * </summary>
     */
    public static async UniTask<bool> IsMyFriend(string _id)
    {
        var postData = new IsFriend.PostData()
        {
            client =  GameManager.Instance.Player.id,
            partner = _id,
        };

        var res = await API<IsFriend.Response>.Post(IsFriend.FUNC_NAME, JsonUtility.ToJson(postData));
        return res.isFriend;
    }

    /**
     * <summary>
     * フレンド追加
     * </summary>
     */
    public static async UniTask AddMyFriend(string _id)
    {
        var requestPostData = new RequestFriend.PostData()
        {
            client =  GameManager.Instance.Player.id,
            partner = _id,
        };

        var acceptPostData = new AcceptFriend.PostData()
        {
            partner =  GameManager.Instance.Player.id,
            client = _id,
        };

        await API<RequestFriend.Response>.Post(RequestFriend.FUNC_NAME, JsonUtility.ToJson(requestPostData));
        await API<AcceptFriend.Response>.Post(AcceptFriend.FUNC_NAME, JsonUtility.ToJson(acceptPostData));
    }

    /**
     * <summary>
     * フレンド解除
     * </summary>
     */
    public static async UniTask DeleteMyFriend(string _id)
    {
        var postData = new DeleteFriend.PostData()
        {
            client =  GameManager.Instance.Player.id,
            partner = _id,
        };

        await API<DeleteFriend.Response>.Post(DeleteFriend.FUNC_NAME, JsonUtility.ToJson(postData));
    }

}
