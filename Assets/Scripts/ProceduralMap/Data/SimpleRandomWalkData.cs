using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to create a custom asset menu item in the Unity Editor
[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]
// Define a class named SimpleRandomWalkData that inherits from ScriptableObject
public class SimpleRandomWalkData : ScriptableObject
{
    // Public fields to define parameters for the simple random walk algorithm
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
