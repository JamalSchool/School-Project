using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ProceduralGenerationAlgorithms;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        // Start the procedural generation process by generating corridors
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        // Create hash sets to store floor positions and potential room positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        // Generate corridors
        CreateCorridors(floorPositions, potentialRoomPositions);

        // Create rooms based on potential room positions
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Find all dead ends in the dungeon
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        // Create rooms at dead ends
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        // Merge room positions with floor positions
        floorPositions.UnionWith(roomPositions);

        // Paint floor tiles and create walls
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }


    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        // Iterate through each dead end position
        foreach (var position in deadEnds)
        {
            // Check if the room floors do not contain the position
            if (roomFloors.Contains(position) == false)
            {
                // Generate a room using random walk starting from the dead end position
                var room = RunRandomWalk(randomWalkParameters, position);

                // Add the generated room to the existing room floors
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        // Initialize a list to store dead end positions
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        // Iterate through each floor position
        foreach (var position in floorPositions)
        {
            // Initialize a counter to track the number of neighboring floor positions
            int neighboursCount = 0;

            // Check each cardinal direction around the current position
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                // If the neighboring position is also a floor position, increment the counter
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }

            // If the current position has only one neighboring floor position, it is a dead end
            if (neighboursCount == 1)
            {
                deadEnds.Add(position); // Add the dead end position to the list
            }
        }

        return deadEnds; // Return the list of dead end positions
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        // Initialize a hash set to store room positions
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        // Calculate the number of rooms to create based on the percentage of potential room positions
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        // Select a random subset of potential room positions to create rooms
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        // Iterate through each selected room position
        foreach (var roomPosition in roomsToCreate)
        {
            // Generate a room using random walk starting from the selected room position
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);

            // Add the generated room to the room positions
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions; // Return the set of room positions
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        // Initialize the current position to the starting position
        var currentPosition = startPosition;

        // Add the starting position to the potential room positions
        potentialRoomPositions.Add(currentPosition);

        // Generate corridors
        for (int i = 0; i < corridorCount; i++)
        {
            // Generate a random walk corridor starting from the current position
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);

            // Update the current position to the end of the corridor
            currentPosition = corridor[corridor.Count - 1];

            // Add the current position to the potential room positions
            potentialRoomPositions.Add(currentPosition);

            // Add the corridor to the floor positions
            floorPositions.UnionWith(corridor);
        }
    }
}
