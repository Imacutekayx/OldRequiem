/// <summary>
/// Class which represents an image added to the scene
/// </summary>
public class LayerImage
{
    //Variables
    public string name;
    public int weight;
    public int height;
    public int x;
    public int y;
    public byte face = 0; //0=N/1=S/2=E/3=W
    //public Sprite img;

    //Constructor
    public LayerImage(string _name, int _weight, int _height, int _x, int _y, byte _face = 0)
	{
        name = _name;
        weight = _weight;
        height = _height;
        x = _x;
        y = _y;
        face = _face;
	}
}
