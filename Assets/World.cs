using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public World(int width = 64, int height = 64) {
        Width = width;
        Height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log($"World created with {tiles.Length} tiles.");
    }

    public Tile GetTileAt(int x, int y) {
        // trying to retrieve a tile outside the bounds of the world
        if (x > Width || x < 0 || y > Height || y < 0) {
            Debug.LogError($"GetTileAt: Tile ({x}, {y}) out of range!");
            return null;
        }
        return tiles[x, y];
    }
}
