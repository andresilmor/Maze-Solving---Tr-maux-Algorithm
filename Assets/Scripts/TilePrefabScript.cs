using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace MazeSolvingAlgorithmTremaux {

    public class TilePrefabScript : MonoBehaviour
    {

    public Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
        {

            //GetComponent<Tile>().type 
        }


        void InitTilemapData()
        {
            TileBase[] allTiles;
            BoundsInt bounds = tileMap.cellBounds;
            allTiles = tileMap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    Vector3Int localPlace = (new Vector3Int(x, y, (int)tileMap.transform.position.y));
                    Vector3 place = tileMap.CellToWorld(localPlace);

                    if (tile != null)
                    {
                        if (tile.name == "red")
                        {
                         //   beginTile = place;
                        }
                        if (tile.name == "green")
                        {
                           // endTile = place;
                        }
                        Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    }
                    else
                    {
                        Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
