using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * ゲームのすべての管理をするクラス
 * </summary>
 */
public sealed class GameManager : Singleton<GameManager>
{
    public string EmailAddress { get; private set; }
}
