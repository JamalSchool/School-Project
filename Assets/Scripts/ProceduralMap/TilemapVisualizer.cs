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
        // Convert binary string representation to integer
        int typeAsInt = Convert.ToInt32(binaryType, 2);

        // Initialize tile to null
        TileBase tile = null;

        // Check if the typeAsInt matches any known wall configurations
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            // If the type matches a wall top configuration, set the tile to wallTop
            tile = wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            // If the type matches a wall side right configuration, set the tile to wallSideRight
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            // If the type matches a wall side left configuration, set the tile to wallSideLeft
            tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            // If the type matches a wall bottom configuration, set the tile to wallBottom
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            // If the type matches a full wall configuration, set the tile to wallFull
            tile = wallFull;
        }

        // If a tile is found, paint it on the wall tilemap at the specified position
        if (tile != null)
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

    // Method to clear all tiles from both the wall and floor tilemaps
    public void Clear()
    {
        // Clear all tiles from the wall tilemap
        wallTilemap.ClearAllTiles();
        // Clear all tiles from the floor tilemap
        floorTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        // Convert binary string representation to integer
        int typeAsInt = Convert.ToInt32(binaryType, 2);

        // Initialize tile to null
        TileBase tile = null;

        // Check if the typeAsInt matches any known corner wall configurations
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            // If the type matches an inner corner down left configuration, set the tile to wallInnerCornerDownLeft
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            // If the type matches an inner corner down right configuration, set the tile to wallInnerCornerDownRight
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            // If the type matches a diagonal corner down left configuration, set the tile to wallDiagonalCornerDownLeft
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            // If the type matches a diagonal corner down right configuration, set the tile to wallDiagonalCornerDownRight
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            // If the type matches a diagonal corner up right configuration, set the tile to wallDiagonalCornerUpRight
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            // If the type matches a diagonal corner up left configuration, set the tile to wallDiagonalCornerUpLeft
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            // If the type matches a full wall with eight directions configuration, set the tile to wallFull
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            // If the type matches a bottom wall with eight directions configuration, set the tile to wallBottom
            tile = wallBottom;
        }

        // If a tile is found, paint it on the wall tilemap at the specified position
        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
