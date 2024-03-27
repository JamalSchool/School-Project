using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public static class WallGenerator
{
    // Method to generate walls based on floor positions and paint them on the tilemap
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualiser)
    {
        // Find positions of basic walls in cardinal directions
        var basicWallPositions = FindWallsInDirections(floorPositions, ProceduralGenerationAlgorithms.Direction2D.cardinalDirectionsList);
        // Find positions of corner walls in diagonal directions
        var cornerWallPositions = FindWallsInDirections(floorPositions, ProceduralGenerationAlgorithms.Direction2D.diagonalDirectionsList);

        // Create basic walls based on found positions
        CreateBasicWall(tilemapVisualiser, basicWallPositions, floorPositions);
        // Create corner walls based on found positions
        CreateCornerWalls(tilemapVisualiser, cornerWallPositions, floorPositions);
    }

    // Method to create corner walls and paint them on the tilemap
    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualiser, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        // Iterate through each corner wall position
        foreach (var position in cornerWallPositions)
        {
            // Initialize a string to represent the configuration of neighboring floor positions
            string neighboursBinaryType = "";

            // Check each direction around the corner wall position
            foreach (var direction in ProceduralGenerationAlgorithms.Direction2D.eightDirectionsList)
            {
                var neighbourPostion = position + direction;

                // If the neighbor position is a floor position, add '1' to the string, otherwise add '0'
                neighboursBinaryType += floorPositions.Contains(neighbourPostion) ? "1" : "0";
            }

            // Paint the single corner wall based on the calculated configuration
            tilemapVisualiser.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    // Method to create basic walls and paint them on the tilemap
    private static void CreateBasicWall(TilemapVisualizer tilemapVisualiser, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        // Iterate through each basic wall position
        foreach (var position in basicWallPositions)
        {
            // Initialize a string to represent the configuration of neighboring floor positions
            string neighboursBinaryValue = "";

            // Check each cardinal direction around the basic wall position
            foreach (var direction in ProceduralGenerationAlgorithms.Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;

                // If the neighbor position is a floor position, add '1' to the string, otherwise add '0'
                neighboursBinaryValue += floorPositions.Contains(neighbourPosition) ? "1" : "0";
            }

            // Paint the single basic wall based on the calculated configuration
            tilemapVisualiser.PaintSingleBasicWall(position, neighboursBinaryValue);
        }
    }

    // Method to find wall positions surrounding floor positions in specified directions
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        // Initialize a HashSet to store wall positions
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        // Iterate through each floor position
        foreach (var position in floorPositions)
        {
            // Iterate through each direction in the specified list
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;

                // If the neighbor position is not a floor position, add it to the wall positions
                if (!floorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        // Return the set of wall positions
        return wallPositions;
    }
}

