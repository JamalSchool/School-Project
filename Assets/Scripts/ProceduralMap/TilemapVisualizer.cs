using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap; // Declares serialized fields to hold references to the floor and wall tilemaps.

    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft; //Declares serialized fields to hold references to various types of tiles used for painting walls.

    // Method to paint floor tiles on the floor tilemap based on the provided floor positions
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        // Call the PaintTiles method to paint tiles on the floor tilemap using the provided floor positions and floor tile
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }


    // Method to paint multiple tiles on the specified tilemap at the given positions with the specified tile
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        // Iterate through each position in the provided collection of positions
        foreach (var position in positions)
        {
            // Paint a single tile on the tilemap at the current position with the specified tile
            PaintSingleTile(tilemap, tile, position);
        }
    }


    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;

        }else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if(tile!=null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
        
    }

    // Method to paint a single tile on the specified tilemap at the given position with the specified tile
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        // Convert the 2D position to a cell position on the tilemap
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);

        // Set the tile at the calculated cell position on the tilemap
        tilemap.SetTile(tilePosition, tile);
    }


    public void Clear()
    {
        wallTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile=wallInnerCornerDownLeft;
        }
        else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        if (tile!=null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
