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
     * </sumamry>
     */
    public static async UniTask TransitScene(SceneType type)
    {
        await SceneManager.LoadSceneAsync((int)type);
    }
}
