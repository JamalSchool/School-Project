using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour // Defines an abstract class named AbstractDungeonGenerator that inherits from MonoBehaviour
{
    // Serialized field to hold a reference to the TilemapVisualizer component
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;

    // Serialized field to specify the starting position of the dungeon generation process
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    // Method to generate the dungeon
    public void GenerateDungeon()
    {
        // Clear the existing tilemap before generating the dungeon
        tilemapVisualizer.Clear();

        // Run the procedural generation process
        RunProceduralGeneration();
    }

    // Abstract method to run the procedural generation process, must be implemented by derived classes
    protected abstract void RunProceduralGeneration();
}

