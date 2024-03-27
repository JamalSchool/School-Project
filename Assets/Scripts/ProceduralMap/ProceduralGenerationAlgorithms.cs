using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;


public static class ProceduralGenerationAlgorithms // this algortithm will be available for any other calsses that want access to this.
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        // Create a HashSet to store the path of the random walk
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // Add the starting position to the path
        path.Add(startPosition);


        // Store the starting position as the previous position
        var previousposition = startPosition;

        // Loop through the walk length to generate the random walk
        for (int i = 0; i < walkLength; i++)
        {
            // Get a random cardinal direction (up, down, left, or right)
            var newPosition = previousposition + Direction2D.GetRandomCardinalDirection();

            // Add the new position to the path
            path.Add(newPosition);

            // Update the previous position to the new position for the next iteration
            previousposition = newPosition;
        }

        // Return the generated path of the random walk
        return path;
    }



    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        // Create a new list to store the corridor positions
        List<Vector2Int> corridor = new List<Vector2Int>();

        // Get a random cardinal direction
        var direction = Direction2D.GetRandomCardinalDirection();

        // Set the current position to the starting position
        var currentPosition = startPosition;

        // Add the starting position to the corridor
        corridor.Add(currentPosition);

        // Iterate over the corridor length
        for (int i = 0; i < corridorLength; i++)
        {
            // Move to the next position in the current direction
            currentPosition += direction;

            // Add the new position to the corridor
            corridor.Add(currentPosition);
        }

        // Return the generated corridor
        return corridor;
    }


    public static List<BoundsInt> BinarySpacePartioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        // Initialize a queue to store rooms waiting to be split
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();

        // Initialize a list to store the final partitioned rooms
        List<BoundsInt> roomsList = new List<BoundsInt>();

        // Enqueue the initial space to split
        roomsQueue.Enqueue(spaceToSplit);

        // Process each room in the queue
        while (roomsQueue.Count > 0)
        {
            // Dequeue a room from the queue
            var room = roomsQueue.Dequeue();

            // Check if the room's dimensions meet the minimum width and height requirements
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                // Randomly choose whether to split horizontally or vertically
                if (UnityEngine.Random.value < 0.5f)
                {
                    // If splitting horizontally is feasible, split the room horizontally
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    // If splitting vertically is feasible, split the room vertically
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    // If neither horizontal nor vertical split is feasible, add the room to the final list
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    // If splitting vertically is feasible, split the room vertically
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    // If splitting horizontally is feasible, split the room horizontally
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    // If neither vertical nor horizontal split is feasible, add the room to the final list
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        // Return the list of partitioned rooms
        return roomsList;
    }


    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        // Randomly select a point to split the room along the x-axis
        var xSplit = UnityEngine.Random.Range(1, room.size.x);

        // Create two new rooms based on the split position
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        // Enqueue the newly created rooms into the rooms queue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        // Randomly select a point to split the room along the y-axis
        var ySplit = UnityEngine.Random.Range(1, room.size.y);

        // Create two new rooms based on the split position
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        // Enqueue the newly created rooms into the rooms queue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }


    public static class Direction2D
    {
        // Define a list of cardinal directions as Vector2Int values
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0) //LEFT
    };

        public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1,-1), //DOWN-LEFT
        new Vector2Int(-1,1) //LEFT-UP
    };

        public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWN-LEFT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(-1,1) //LEFT-UP
    };


        public static Vector2Int GetRandomCardinalDirection()
        {
            // Return a random cardinal direction from the cardinalDirectionsList
            return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
        }
    } 
}
