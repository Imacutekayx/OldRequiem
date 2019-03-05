using System.Collections.Generic;

/// <summary>
/// Class which represents a script on a scene
/// </summary>
public class LayerScript
{
    //Variable
    public string name;
    public bool state;
    public int weight;
    public int height;
    public int x;
    public int y;
    public int range;
    public List<string> parameters;

    //Constructor
    public LayerScript(string _name, bool _state, int _weight, int _height, int _x, int _y, int _range, List<string> _parameters = null)
	{
        name = _name;
        state = _state;
        weight = _weight;
        height = _height;
        x = _x;
        y = _y;
        range = _range;
        parameters = _parameters;
	}
}
