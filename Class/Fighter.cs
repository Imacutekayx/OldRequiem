﻿using System.Collections.Generic;
using Tester;
using Tester.Class;

/// <summary>
/// Parent class of all fighting entity
/// </summary>
public class Fighter : Entity
{
    //Combat
    public int hp;
    public int mp;
    public int[] dices;
    public Weapon[] weapon;
    public string weapontype;
    public List<Power> powers;
    public Armor[] armor;
    public bool[] armorChange;
    public string armortype;

    public void Attack()
    {
        //TODO Attack with weapon
    }
}