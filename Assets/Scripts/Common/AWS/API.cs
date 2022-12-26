using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public static class API<T>
{
    private const string URL = "https://mpvi4fz94b.execute-api.ap-northeast-1.amazonaws.com/";

    public static async UniTask<T> Post(string funcName, string postData)
    {
        string url = URL + funcName;

        using (UnityWebRequest request = UnityWebRequest.Post(url, postData))
        {
            request.SetRequestHeader("Authorization", GameManager.Instance.Session.IdToken);

            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                default:
                    Debug.LogError("取得できませんでした．");
                    return default(T);
            }
        }
    }

    public static async UniTask<T> Get(string funcName)
    {
        string url = URL + funcName;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", GameManager.Instance.Session.IdToken);

            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                default:
                    Debug.LogError("取得できませんでした．");
                    return default(T);
            }
        }
    }
}

public static class GetTasks
{
    public const string FUNC_NAME = "get_tasks";

    [Serializable]
    public class PostData
    {
        public string id;
    }

    [Serializable]
    public class Response
    {
        public List<Task> tasks;
    }
}

public static class CreateTask
{
    public const string FUNC_NAME = "create_task";

    [Serializable]
    public class PostData
    {
        public List<Task> tasks;
    }

    [Serializable]
    public class Response
    {

    }
}

public static class DecodeIdtoken
{
    public const string FUNC_NAME = "decode_idtoken";

    [Serializable]
    public class Response
    {
        public string id;
        public string nickname;
        public string email;
        public List<string> groupNames;
    }
}
