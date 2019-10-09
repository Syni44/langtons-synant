using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    World world;
    Tile tileOnNow;

    [SerializeField]
    public float Speed = 1;

    [SerializeField]
    public bool LeftOnWhite = true;
    [SerializeField]
    public bool LeftOnRed = true;
    [SerializeField]
    public bool LeftOnBlue = true;
    [SerializeField]
    public bool LeftOnGreen = true;
    [SerializeField]
    public bool LeftOnYellow = true;
    [SerializeField]
    public bool LeftOnPurple = true;

    private Vector3 direction;
    private int iterations = 0;

    void OnCollisionEnter2D(Collision2D collision) {
        iterations++;

        // throw an error log if the collision hitbox is missed last iteration
        if (Math.Abs(tileOnNow.X) - Math.Abs(world.GetTileAt((int)collision.transform.position.x, (int)collision.transform.position.y).X) > 1.5
            || Math.Abs(tileOnNow.Y) - Math.Abs(world.GetTileAt((int)collision.transform.position.x, (int)collision.transform.position.y).Y) > 1.5) {
            Debug.LogError($"Skipped a tile!! consider this pattern imperfect - ({iterations})");
        }

        Debug.Log($"tile reached: iteration ({iterations})");

        // get tile object ant has reached
        tileOnNow = world.GetTileAt((int)collision.transform.position.x, (int)collision.transform.position.y);

        // check tile color and determine new direction to move, along with changing the color of the tile
        switch (tileOnNow.TileColor) {
            case Tile.TileColors.White:
                direction = ChangeDirection(direction, LeftOnWhite);
                world.GetTileAt(tileOnNow.X, tileOnNow.Y).TileColor = Tile.TileColors.Red;
                break;
            case Tile.TileColors.Red:
                direction = ChangeDirection(direction, LeftOnRed);
                world.GetTileAt(tileOnNow.X, tileOnNow.Y).TileColor = Tile.TileColors.Blue;
                break;
            case Tile.TileColors.Blue:
                direction = ChangeDirection(direction, LeftOnBlue);
                world.GetTileAt(tileOnNow.X, tileOnNow.Y).TileColor = Tile.TileColors.Green;
                break;
            case Tile.TileColors.Green:
                direction = ChangeDirection(direction, LeftOnGreen);
                world.GetTileAt(tileOnNow.X, tileOnNow.Y).TileColor = Tile.TileColors.Yellow;
                break;
            case Tile.TileColors.Yellow:
                direction = ChangeDirection(direction, LeftOnYellow);
                world.GetTileAt(tileOnNow.X, tileOnNow.Y).TileColor = Tile.TileColors.Purple;
                break;
            case Tile.TileColors.Purple:
                direction = ChangeDirection(direction, LeftOnPurple);
                world.GetTileAt(tileOnNow.X, tileOnNow.Y).TileColor = Tile.TileColors.White;
                break;
            default:
                Debug.LogError("Ant.OnCollisionEnter: oops tilecolor check switch statement");
                break;
        }

        // round ant position
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    // Start is called before the first frame update
    void Start() {
        world = WorldController.world;
        transform.position = new Vector2(world.Width / 2, world.Height / 2);
        tileOnNow = world.GetTileAt((int)transform.position.x, (int)transform.position.y);
        direction = Vector3.up / (32 / Speed);
    }

    // Update is called once per frame
    void Update() {
        if (world != null) {
            Move();
        }
    }

    void Move() {
        transform.position += direction;
    }

    Vector3 ChangeDirection(Vector3 incomingDirection, bool turnedLeft) {
        if (incomingDirection == Vector3.up / (32 / Speed)) {
            if (turnedLeft)
                return Vector3.left / (32 / Speed);

            return Vector3.right / (32 / Speed);
        }
        else if (incomingDirection == Vector3.left / (32 / Speed)) {
            if (turnedLeft)
                return Vector3.down / (32 / Speed);

            return Vector3.up / (32 / Speed);
        }
        else if (incomingDirection == Vector3.down / (32 / Speed)) {
            if (turnedLeft)
                return Vector3.right / (32 / Speed);

            return Vector3.left / (32 / Speed);
        }
        else if (incomingDirection == Vector3.right / (32 / Speed)) {
            if (turnedLeft)
                return Vector3.up / (32 / Speed);

            return Vector3.down / (32 / Speed);
        }
        else {
            Debug.LogError("Ant.ChangeDirection: got an invalid direction?? oops???");
            return Vector3.zero;
        }
    }
}