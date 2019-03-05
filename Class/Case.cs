/// <summary>
/// Class which represents a case
/// </summary>
public class Case
{
    //Variables
    public int x;
    public int y;
    public byte face; //0=S/1=N/2=E/3=W
    public string type;
    public string state;
    public string script;

    //Constructor
	public Case(int _x, int _y, string _type, string _state, string _script)
	{
        x = _x;
        y = _y;
        type = _type;
        state = _state;
        script = _script;
	}
}
