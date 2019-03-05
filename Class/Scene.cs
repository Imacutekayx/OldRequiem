﻿using System.Collections.Generic;

/// <summary>
/// Class which represents a scene
/// </summary>
public class Scene
{
    //Variables
    public int weight;
    public int height;
    public string gamemode;

    //Objects
    public List<LayerImage> background;
    public List<LayerImage> adds0;
    public List<LayerImage> adds1;
    public List<LayerImage> adds2;
    public List<Case> cases;
    public List<LayerScript> scripts;
    public List<Entity> entities;

    //Constructor
	public Scene(int _weight, int _height, string _gamemode, List<LayerImage> _background, List<LayerImage> _adds0, 
        List<LayerImage> _adds1, List<LayerImage> _adds2, List<Case> _cases, List<LayerScript> _scripts, List<Entity> _entities)
	{
        weight = _weight;
        height = _height;
        gamemode = _gamemode;
        background = _background;
        adds0 = _adds0;
        adds1 = _adds1;
        adds2 = _adds2;
        cases = _cases;
        scripts = _scripts;
        entities = _entities;
	}
}
