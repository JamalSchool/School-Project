using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4; // Minimum width and height of a room, serialized for adjustment in the Unity Inspector.

    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20; // Width and height of the dungeon area, serialized for adjustment in the Unity Inspector.

    [SerializeField]
    [Range(0, 10)]
    private int offset = 1; // Offset value used for creating rooms, serialized for adjustment in the Unity Inspector.

    [SerializeField]
    private bool randomWalkRooms = false; // Indicates whether to create rooms using random walk algorithm, serialized for adjustment in the Unity Inspector.

    protected override void RunProceduralGeneration()
    {
        CreateRooms(); // Method call to generate rooms.
    }

    private void CreateRooms()
    {
        // Generate a list of rooms using binary space partitioning algorithm.
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int
            (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // Initialize a hash set to store floor tile positions.

        // Create rooms either using random walk algorithm or simple room generation algorithm.
        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList); // Generate rooms using random walk algorithm.
        }
        else
        {
            floor = CreateSimpleRooms(roomsList); // Generate rooms using simple room generation algorithm.
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>(); // Initialize a list to store room center positions.

        // Populate the list of room centers.
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        // Connect the rooms with corridors.
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

        // Combine floor positions with corridor positions.
        floor.UnionWith(corridors);

        // Paint floor tiles on the tilemap.
        tilemapVisualizer.PaintFloorTiles(floor);

        // Create walls around the generated rooms and corridors.
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // Initialize a hash set to store floor tile positions.

        // Iterate through each room generated by binary space partitioning.
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i]; // Get the bounds of the current room.

            // Calculate the center of the room and round the values to the nearest integers.
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));

            // Generate the floor tiles of the room using a random walk algorithm.
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            // Iterate through each position in the room's floor tiles.
            foreach (var position in roomFloor)
            {
                // Check if the position lies within the room's bounds with the specified offset.
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                    position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position); // Add the position to the floor hash set.
                }
            }
        }
        return floor; // Return the set of floor positions of all rooms.
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>(); // Initialize a hash set to store corridor tile positions.

        // Select a random room center as the starting point for corridor generation.
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter); // Remove the selected room center from the list.

        // Iterate until all rooms are connected.
        while (roomCenters.Count > 0)
        {
            // Find the closest room center to the current one.
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest); // Remove the closest room center from the list.

            // Create a corridor between the current room and the closest one.
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

            currentRoomCenter = closest; // Update the current room center to the closest one.
            corridors.UnionWith(newCorridor); // Add the new corridor positions to the set.
        }
        return corridors; // Return the set of corridor positions connecting all rooms.
    }


    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>(); // Initialize a hash set to store corridor tile positions.
        var position = currentRoomCenter; // Set the current position to the starting room center.
        corridor.Add(position); // Add the starting position to the corridor.

        // Loop until the current position reaches the destination vertically.
        while (position.y != destination.y)
        {
            // Move the position vertically towards the destination.
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position); // Add the updated position to the corridor.
        }

        // Loop until the current position reaches the destination horizontally.
        while (position.x != destination.x)
        {
            // Move the position horizontally towards the destination.
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position); // Add the updated position to the corridor.
        }
        return corridor; // Return the set of positions forming the corridor.
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero; // Initialize the closest point as the origin.
        float distance = float.MaxValue; // Initialize the minimum distance to a very large value.

        // Iterate through each room center to find the closest one to the current room center.
        foreach (var position in roomCenters)
        {
            // Calculate the distance between the current room center and the current position.
            float currentDistance = Vector2.Distance(position, currentRoomCenter);

            // If the current distance is less than the previous minimum distance, update the closest point and the minimum distance.
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest; // Return the closest point to the current room center.
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // Initialize a hash set to store floor tile positions.

        // Iterate through each room in the list of room bounds.
        foreach (var room in roomsList)
        {
            // Iterate through each row and column within the room bounds.
            for (int col = 0; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    // Calculate the position within the room.
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(row, col);
                    floor.Add(position); // Add the position to the floor hash set.
                }
            }
        }
        return floor; // Return the set of positions forming the rooms.
    }

}
