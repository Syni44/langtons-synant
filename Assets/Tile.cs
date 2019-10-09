using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile
{
    public enum TileColors { White, Red, Blue, Green, Yellow, Purple };

    Action<Tile> TileColorChangedEvent;

    private TileColors _color;
    public TileColors TileColor {
        get {
            return _color;
        }
        set {
            _color = value;

            TileColorChangedEvent?.Invoke(this);
        }
    }

    private readonly Ant ant;
    private readonly World world;
    public int X { get; private set; }
    public int Y { get; private set; }

    public Tile(World world, int x, int y) {
        this.world = world;
        X = x;
        Y = y;
    }

    public void RegisterTileColorChangedEvent(Action<Tile> a)
        => TileColorChangedEvent += a;

}
