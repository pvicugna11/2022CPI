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
     * タスク情報を取得する
     * </summary>
     */
    public static async UniTask GetUserTasks(string _id)
    {
        var postData = new GetTasks.PostData()
        {
            id = _id,
        };

        var res = await API<GetTasks.Response>.Post(GetTasks.FUNC_NAME, JsonUtility.ToJson(postData));
        GameManager.Instance.Tasks = res.tasks;
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
}
