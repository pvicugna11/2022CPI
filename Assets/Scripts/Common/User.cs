using System;

[Serializable]
public class User
{
    public User() {}
    public User(string _id)
    {
        id = _id;
    }

    public string id;
    public string name;

    public void Fetch(GetUserData.Response res)
    {
        id = res.id;
        name = res.nickname;
    }
}
