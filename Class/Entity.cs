using System.Collections.Generic;
using Tester.Class;

/// <summary>
/// Parent class of entity
/// </summary>
public class Entity
{
    //Basic
    public string name;
    public bool sex = true; //true = male
    public int age = 0;
    public string type;
    public byte face = 0; //0=S/1=N/2=E/3=W
    public int x;
    public int y;
    public Dictionary<Item, int> bag = new Dictionary<Item, int>();
}
