using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Types of Lasers:
/// <list type="number">
/// <item>Single</item>
/// <item>Tri</item>
/// <item>Spread</item>
/// <item>ABSOLUTE Spread</item>
/// </list>
/// </summary>

public enum LaserType { Single, Tri, Spread, ABSSpread }

// Inheritance
public class Laser : PickUps
{
    public LaserType laserType;
}
