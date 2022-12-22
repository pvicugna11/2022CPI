using System.Collections.Generic;
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
    public string Email { get; set; }
    public string Nickname { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
    
    public void GetMyInfo()
    {
        
    }
}
