using System;

namespace ShoppingProgramming;

public abstract class User
{   
    
    public string Name {  get; set; }   
    public DateTime LoginTime { get; set;}

    protected User(string name)
    {
        Name = name;
        LoginTime = DateTime.Now;
    }


}
