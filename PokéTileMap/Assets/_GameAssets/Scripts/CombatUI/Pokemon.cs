using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{

    public readonly Sprite[] Images; //Elemento 0 es la espalda, el 1 de frente

    public readonly string Name;

    public readonly int totalHP;
    public int currentHP { get; private set; }

    public readonly int Atk;
    public readonly int Def;

    public int Level { get; private set; }



    public Pokemon(Sprite back, Sprite front, string name, int totalHP, int Atk, int Def, int Level)
    {
        Images = new Sprite[2];
        this.Images[0] = back;
        this.Images[1] = front;

        this.Name = name;
        this.totalHP = totalHP;
        this.currentHP = totalHP;
        this.Atk = Atk;
        this.Def = Def;
        this.Level = Level;
    }
    public Pokemon(Sprite[] sprites, string name, int totalHP, int Atk, int Def, int Level)
    {
        this.Images = sprites;
        this.Name = name;
        this.totalHP = totalHP;
        this.currentHP = totalHP;
        this.Atk = Atk;
        this.Def = Def;
        this.Level = Level;
    }


}
