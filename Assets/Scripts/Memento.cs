using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento
{
    Vector2 position;
    int health;
    int lifes;
    int coins;
    bool doubleJump;
    int xp;
    int lvl;

    public Memento(Vector2 position, int health, int lifes, int coins, bool doubleJump, int xp, int lvl)
    {
        this.Position = position;
        this.Health = health;
        this.Lifes = lifes;
        this.Coins = coins;
        this.DoubleJump = doubleJump;
        this.Xp = xp;
        this.Lvl = lvl;

    }

    public override string ToString()
    {
        return position.ToString() + ";" + health.ToString() + ";" + lifes.ToString() + ";" + coins.ToString() + ";" + doubleJump.ToString() + ";" + xp.ToString() + ";" + lvl.ToString();
    }

    public Vector2 Position { get => position; private set => position = value; }
    public int Health { get => health; private set => health = value; }
    public int Lifes { get => lifes; private set => lifes = value; }
    public int Coins { get => coins; private set => coins = value; }
    public bool DoubleJump { get => doubleJump; private set => doubleJump = value; }
    public int Xp { get => xp; private set => xp = value; }
    public int Lvl { get => lvl; private set => lvl = value; }
}
