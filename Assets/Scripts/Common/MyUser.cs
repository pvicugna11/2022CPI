using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyUser
{
    public string id;
    public string nickname;
    public string email;
    public List<string> groupNames;

    public void SetMyUser(DecodeIdtoken.Response res)
    {
        id = res.id;
        nickname = res.nickname;
        email = res.email;
        groupNames = res.groupNames;
    }
}
