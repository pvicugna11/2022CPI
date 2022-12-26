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
        if (GameManager.Instance.myUser == null) { GameManager.Instance.myUser = new MyUser(); }

        GameManager.Instance.myUser.SetMyUser(await API<DecodeIdtoken.Response>.Get(DecodeIdtoken.FUNC_NAME));
    }
}
