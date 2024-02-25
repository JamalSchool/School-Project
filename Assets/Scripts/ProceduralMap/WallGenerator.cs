using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualiser)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWall(tilemapVisualiser, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualiser, cornerWallPositions, floorPositions);

    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualiser, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPostion = position + direction;
                if(floorPositions.Contains(neighbourPostion))
                {
                    neighboursBinaryType += "1";

                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualiser.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualiser, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryValue = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryValue += "1";
                }
                else
                {
                    neighboursBinaryValue+= "0";
                }
            }
            tilemapVisualiser.PaintSingleBasicWall(position, neighboursBinaryValue);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var position in floorPositions) 
        {
            foreach(var direction in directionList)
            {
                var neighbourPosition= position+direction;
                if (floorPositions.Contains(neighbourPosition)==false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
