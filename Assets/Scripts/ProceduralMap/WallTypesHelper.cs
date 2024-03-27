using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   

public static class WallTypesHelper
{
    // HashSet to represent the different configurations of the top wall
    public static HashSet<int> wallTop = new HashSet<int>
    {
        // Binary representations of different top wall configurations
        0b1111,
        0b0110,
        0b0011,
        0b0010,
        0b1010,
        0b1100,
        0b1110,
        0b1011,
        0b0111
    };
    // HashSet to represent the left side wall
    public static HashSet<int> wallSideLeft = new HashSet<int>
    {
        // Binary representation of the left side wall
        0b0100
    };

    // HashSet to represent the right side wall
    public static HashSet<int> wallSideRight = new HashSet<int>
    {
        // Binary representation of the right side wall
        0b0001
    };

    // HashSet to represent the bottom wall
    public static HashSet<int> wallBottm = new HashSet<int>
    {
        // Binary representation of the bottom wall
        0b1000
    };

    // HashSet to represent the inner corner walls at the bottom left
    public static HashSet<int> wallInnerCornerDownLeft = new HashSet<int>
    {
         // Binary representations of different inner corner walls at the bottom left
        0b11110001,
        0b11100000,
        0b11110000,
        0b11100001,
        0b10100000,
        0b01010001,
        0b11010001,
        0b01100001,
        0b11010000,
        0b01110001,
        0b00010001,
        0b10110001,
        0b10100001,
        0b10010000,
        0b00110001,
        0b10110000,
        0b00100001,
        0b10010001
    };
    // HashSet to represent the inner corner walls at the bottom right
    public static HashSet<int> wallInnerCornerDownRight = new HashSet<int>
    {
         // Binary representations of different inner corner walls at the bottom right
        0b11000111,
        0b11000011,
        0b10000011,
        0b10000111,
        0b10000010,
        0b01000101,
        0b11000101,
        0b01000011,
        0b10000101,
        0b01000111,
        0b01000100,
        0b11000110,
        0b11000010,
        0b10000100,
        0b01000110,
        0b10000110,
        0b11000100,
        0b01000010

    };
    // HashSet to represent the diagonal corner walls at the bottom left
    public static HashSet<int> wallDiagonalCornerDownLeft = new HashSet<int>
    {
       // Binary representation of the diagonal corner wall at the bottom left
        0b01000000
    };

    // HashSet to represent the diagonal corner walls at the bottom right
    public static HashSet<int> wallDiagonalCornerDownRight = new HashSet<int>
    {
        // Binary representation of the diagonal corner wall at the bottom right
        0b00000001
    };

    // HashSet to represent the diagonal corner walls at the top left
    public static HashSet<int> wallDiagonalCornerUpLeft = new HashSet<int>
    {
         // Binary representations of different diagonal corner walls at the top left
        0b00010000,
        0b01010000,
    };

    // HashSet to represent the diagonal corner walls at the top right
    public static HashSet<int> wallDiagonalCornerUpRight = new HashSet<int>
    {
        // Binary representations of different diagonal corner walls at the top right
        0b00000100,
        0b00000101
    };

    // HashSet to represent the full walls
    public static HashSet<int> wallFull = new HashSet<int>
    {
        // Binary representations of different full walls
        0b1101,
        0b0101,
        0b1101,
        0b1001

    };

    // HashSet to represent the full walls with eight directions
    public static HashSet<int> wallFullEightDirections = new HashSet<int>
    {
        // Binary representations of different full walls with eight directions
        0b00010100,
        0b11100100,
        0b10010011,
        0b01110100,
        0b00010111,
        0b00010110,
        0b00110100,
        0b00010101,
        0b01010100,
        0b00010010,
        0b00100100,
        0b00010011,
        0b01100100,
        0b10010111,
        0b11110100,
        0b10010110,
        0b10110100,
        0b11100101,
        0b11010011,
        0b11110101,
        0b11010111,
        0b11010111,
        0b11110101,
        0b01110101,
        0b01010111,
        0b01100101,
        0b01010011,
        0b01010010,
        0b00100101,
        0b00110101,
        0b01010110,
        0b11010101,
        0b11010100,
        0b10010101

    };

    // HashSet to represent the bottom walls with eight directions
    public static HashSet<int> wallBottmEightDirections = new HashSet<int>
    {
        // Binary representation of the bottom wall with eight directions
        0b01000001
    };

}