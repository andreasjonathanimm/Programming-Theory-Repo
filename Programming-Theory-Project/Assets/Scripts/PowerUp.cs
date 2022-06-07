using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Types of PowerUps:
/// <list type="number">
/// <item>Shield</item>
/// <item>Missile</item>
/// <item>INFINITE Laser</item>
/// </list>
/// </summary>

public enum PowerUpType { None, Shield, Missile, Health, STRONK_Laser }

// Inheritance
public class PowerUp : PickUps
{
    public PowerUpType powerUpType;
}
