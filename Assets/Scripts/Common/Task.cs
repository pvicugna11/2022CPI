using System;
using System.Collections.Generic;

[Serializable]
public class Task
{
    public Task(string _name)
    {
        name = string.IsNullOrWhiteSpace(_name) ? "" : _name;
    }

    public string name;
    public bool isFinished;
}
