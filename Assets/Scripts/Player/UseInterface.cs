using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBuy
{
    void Use(Weapon wp);

}
public interface IWeaponPickup
{
    void Use(Weapon wp);

}
public interface Use
{
    void Use();

}


public interface IGrabale
{
    void Grab();
}
