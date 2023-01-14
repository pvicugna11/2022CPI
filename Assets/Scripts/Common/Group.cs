using System;
using System.Collections.Generic;

[Serializable]
public class Group
{
    public string name;
    public List<User> members = new List<User>();
    public Date startDate;

    public void Set(string groupName, GetGroup.Response res)
    {
        name = groupName;
        members = res.members.ConvertAll(x => new User(x));
        startDate = res.startDate;
    }
}
