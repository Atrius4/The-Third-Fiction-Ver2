using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento
{
    Vector2 position;
    int health;

    public Memento(Vector2 position, int health)
    {
        this.Position = position;
        this.Health = health;
    }

    public override string ToString()
    {
        return position.ToString() + ";" + health.ToString();
    }

    public Vector2 Position { get => position; private set => position = value; }
    public int Health { get => health; private set => health = value; }
}
