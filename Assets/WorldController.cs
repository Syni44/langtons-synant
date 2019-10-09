using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{    
    public static World world { get; private set; }
    public Sprite tileSprite;

    // Start is called before the first frame update
    void Start()
    {
        world = new World();
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(world.Width / 2, world.Height / 2, -20);
        GameObject.FindGameObjectWithTag("Background").transform.position = new Vector3(world.Width / 2, world.Height / 2, 0);

        // create a GameObject for each tile in world -- for visuals. tile array already exists in numbers form!
        for (int x = 0; x < world.Width; x++) {
            for (int y = 0; y < world.Height; y++) {
                Tile tileData = world.GetTileAt(x, y);

                GameObject tileObject = new GameObject();
                tileObject.name = $"Tile_{x}_{y}";
                tileObject.transform.position = new Vector3(tileData.X, tileData.Y, 0f);

                BoxCollider2D tileBC = tileObject.AddComponent<BoxCollider2D>();
                tileBC.size -= new Vector2(0.5f, 0.5f);
                tileBC.enabled = true;

                Rigidbody2D tileRB = tileObject.AddComponent<Rigidbody2D>();
                tileRB.bodyType = RigidbodyType2D.Kinematic;
                tileRB.useFullKinematicContacts = true;

                SpriteRenderer tileSR = tileObject.AddComponent<SpriteRenderer>();
                tileSR.sprite = tileSprite;

                tileData.RegisterTileColorChangedEvent( (tile) =>
                    {
                        OnTileColorChanged(tile, tileObject);
                    }
                );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTileColorChanged(Tile tileData, GameObject tileObject) {
        // TODO: see below comment block
        var spriteColor = tileObject.GetComponent<SpriteRenderer>().color;

        switch (tileData.TileColor) {
            case Tile.TileColors.White: spriteColor = Color.white;
                break;
            case Tile.TileColors.Blue: spriteColor = Color.blue;
                break;
            case Tile.TileColors.Red: spriteColor = Color.red;
                break;
            case Tile.TileColors.Green: spriteColor = Color.green;
                break;
            case Tile.TileColors.Yellow: spriteColor = Color.yellow;
                break;
            case Tile.TileColors.Purple: spriteColor = Color.magenta;
                break;
            default: Debug.LogError("oh shit some OnTileColorChanged thing went wrong");
                break;
        }

        // TODO: womp woooomp. C# 8.0 only and unity doesnt want me using that yet i think

        //Color spriteColor = tileData.tileColor switch
        //{
        //    Tile.TileColors.Blue => Color.blue,
        //    Tile.TileColors.Red => Color.red,
        //    Tile.TileColors.Green => Color.green,
        //    Tile.TileColors.Yellow => Color.yellow,
        //    Tile.TileColors.Purple => Color.magenta,
        //    _ => Color.white
        //};

        tileObject.GetComponent<SpriteRenderer>().color = spriteColor;
    }
}
