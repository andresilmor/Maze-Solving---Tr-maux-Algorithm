using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeSolvingAlgorithmTremaux
{
    public class Tile : MonoBehaviour
    {

        public TileType type;

        public enum TileType
        {
            Wall,
            Path,
            Begin,
            End 
        };

        public TileType GetTileType()
        {
            return this.type;
        }

       
        public bool IsWalkable()
        {
            return (this.type == TileType.Path || this.type == TileType.Begin || this.type == TileType.End);
        }

    }
}