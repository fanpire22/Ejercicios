using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{

    public class Skill
    {
        public readonly string Name;
        public readonly int Damage;
        public readonly GameObject ParticleEffect;

        public Skill(string name, int damage, GameObject particleEffect)
        {
            Name = name;
            Damage = damage;
            ParticleEffect = particleEffect;
        }
    }

    public readonly Sprite[] Images; //Elemento 0 es la espalda, el 1 de frente

    public readonly string Name;

    public readonly int totalHP;
    public int CurrentHP { get; private set; }

    public readonly int Atk;
    public readonly int Def;

    public int Level { get; private set; }

    public readonly Skill[] Skills;

    public Pokemon(Sprite back, Sprite front, string name, int totalHP, int Atk, int Def, int Level)
    {
        Images = new Sprite[2];
        this.Images[0] = back;
        this.Images[1] = front;

        this.Name = name;
        this.totalHP = totalHP;
        this.CurrentHP = totalHP;
        this.Atk = Atk;
        this.Def = Def;
        this.Level = Level;
    }
    public Pokemon(Sprite[] sprites, string name, int totalHP, int Atk, int Def, int Level)
    {
        this.Images = sprites;
        this.Name = name;
        this.totalHP = totalHP;
        this.CurrentHP = totalHP;
        this.Atk = Atk;
        this.Def = Def;
        this.Level = Level;
    }

    /// <summary>
    /// Tiramos contra la CA del pokémon (sí, la CA xD)
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="skillUsed"></param>
    public void SetDamage(Pokemon enemy, Skill skillUsed)
    {
       int tirada = Random.Range(1, 21);
        if (tirada == 1)
        {
            //Pifia!
        }
        else if (tirada == 20)
        {
            //Crítico!
            CurrentHP -= skillUsed.Damage * 2;

        }else if ((tirada + enemy.Atk) >= this.Def){
            //Nos han dado
            CurrentHP -= skillUsed.Damage;
        }
        else
        {
            //Han fallado
        }

        if(CurrentHP <= 0)
        {
            //La diñamos
        }

    }

}
