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

        await UniTask.WaitUntil(() => GameManager.Instance.IsLogin);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", GameManager.Instance.Session.IdToken);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(request.downloadHandler.text);
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                default:
                    Debug.LogError(request.result);
                    return default(T);
            }
        }
    }

    public static async UniTask<T> Get(string funcName)
    {
        string url = URL + funcName;

        await UniTask.WaitUntil(() => GameManager.Instance.IsLogin);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", GameManager.Instance.Session.IdToken);

            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(request.downloadHandler.text);
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                default:
                    Debug.LogError(request.result);
                    return default(T);
            }
        }
    }
}

// POST
public static class GetTasks
{
    public const string FUNC_NAME = "task/get_tasks";

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
    public const string FUNC_NAME = "task/create_task";

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

// GET
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

public static class GetOwnTask
{
    public const string FUNC_NAME = "task/get_own_task";

    [Serializable]
    public class Response
    {
        public List<Task> tasks;
    }
}
