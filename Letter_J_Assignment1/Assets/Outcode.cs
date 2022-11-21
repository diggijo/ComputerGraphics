using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Outcode
{
    internal bool up;
    internal bool down;
    internal bool left;
    internal bool right;

    public Outcode()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    public Outcode(Vector2 v)
    {
        up = (v.y > 1);
        down = (v.y < -1);
        left = (v.x < -1);
        right = (v.x > 1);
    }

    public Outcode(bool Up, bool Down, bool Left, bool Right)
    {
        up = Up;
        down = Down;
        left = Left;
        right = Right;
    }

    public static Outcode operator *(Outcode A, Outcode B)
    {
        return new Outcode(A.up && B.up, A.down && B.down, A.left && B.left, A.right && B.right);
    }
    public static Outcode operator +(Outcode A, Outcode B)
    {
        return new Outcode(A.up || B.up, A.down && B.down, A.left && B.left, A.right && B.right);
    }


    public static  bool operator ==(Outcode A, Outcode B)
    {
        return (A.up == B.up) && (A.down == B.down) && (A.left == B.left) && (A.right == B.right);
    }

    public static bool operator !=(Outcode A, Outcode B)
    {
        return !(A == B);
    }


    public void printOutcode(Outcode o)
    {
        string path = "Assets/Outcodes.txt";

        StreamWriter writer = new StreamWriter(path, true);

        String outstring = (up ? "1" : "0") + (down ? "1" : "0") + (left ? "1" : "0") + (right ? "1" : "0");

        writer.WriteLine("Outcode: " + outstring);

        writer.Close();
    }
}
