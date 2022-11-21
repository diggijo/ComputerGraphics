using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    Texture2D ourTexture;
    Outcode inScreen = new Outcode();
    void Start()
    {
        ourTexture = new Texture2D(256, 256);
        Renderer screen = FindObjectOfType<Renderer>();
        screen.material.mainTexture = ourTexture;
        Model my_Model = new Model();
        //my_Model.CreateUnityGameObject();

        List<Vector3> verts = my_Model.vertices;
        printVerts(verts);

        //Rotation Matrix
        Vector3 axis = new Vector3(12, 3, 3);
        axis.Normalize();
        Matrix4x4 rotation_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(12, axis), Vector3.one);
        printMatrix(rotation_matrix);

        //Print New Vertices after Roation
        List<Vector3> image_after_rotation = getImage(verts, rotation_matrix);
        printVerts(image_after_rotation);

        //Scale Matrix
        Vector3 scale_by = new Vector3(4, 2, 3);
        Matrix4x4 scale_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale_by);
        printMatrix(scale_matrix);

        //Print New Vertices after Scale
        List<Vector3> image_after_scale = getImage(image_after_rotation, scale_matrix);
        printVerts(image_after_scale);

        //Translate Matrix
        Vector3 translate_by = new Vector3(-3, 1, 4);
        Matrix4x4 translate_matrix = Matrix4x4.TRS(translate_by, Quaternion.identity, Vector3.one);
        printMatrix(translate_matrix);

        //Print New Vertices after Translation
        List<Vector3> image_after_translation = getImage(image_after_scale, translate_matrix);
        printVerts(image_after_translation);

        //Single Matrix for Transformations
        Matrix4x4 single_matrix_transformations = translate_matrix * scale_matrix * rotation_matrix;
        printMatrix(single_matrix_transformations);

        //Print Vertices using the one matrix for transformations
        List<Vector3> image_after_single_matrix_transformations = getImage(verts, single_matrix_transformations);
        printVerts(image_after_single_matrix_transformations);

        //Viewing Matrix
        Vector3 camPos = new Vector3(14, 6, 53);
        Vector3 camLookAt = new Vector3(3, 12, 3).normalized;
        Vector3 camUp = new Vector3(4, 3, 12).normalized;

        Matrix4x4 viewing_matrix = Matrix4x4.LookAt(camPos, camLookAt, camUp);
        printMatrix(viewing_matrix);

        //Print Image after Viewing Matrix
        List<Vector3> image_after_viewing = getImage(image_after_translation, viewing_matrix);
        printVerts(image_after_viewing);


        //Projection Matrix
        Matrix4x4 proj_matrix = Matrix4x4.Perspective(90, 1, 1, 1000);
        printMatrix(proj_matrix);

        //Print Image after Viewing Matrix
        List<Vector3> image_after_projection = getImage(image_after_viewing, proj_matrix);
        printVerts(image_after_projection);

        //Single Matrix
        Matrix4x4 single_matrix = proj_matrix * viewing_matrix * translate_matrix * scale_matrix * rotation_matrix;
        printMatrix(single_matrix);

        //Print Vertices using the one matrix
        List<Vector3> image_after_single_matrix = getImage(verts, single_matrix);
        printVerts(image_after_single_matrix);



        //Up Left
        Outcode outcodeUL = new Outcode(new Vector2(-7, 2));
        outcodeUL.printOutcode(outcodeUL);
        //Up
        Outcode outcodeU = new Outcode(new Vector2(0, 9));
        outcodeU.printOutcode(outcodeU);
        //Up Right
        Outcode outcodeUR = new Outcode(new Vector2(4, 3.3f));
        outcodeUR.printOutcode(outcodeUR);
        //Left
        Outcode outcodeL = new Outcode(new Vector2(-5, 0));
        outcodeL.printOutcode(outcodeL);
        //Zero
        Outcode outcodeZ = new Outcode(new Vector2(0, 0));
        outcodeZ.printOutcode(outcodeZ);
        //Right
        Outcode outcodeR = new Outcode(new Vector2(1.5f, 0));
        outcodeR.printOutcode(outcodeR);
        //Down Left
        Outcode outcodeDL = new Outcode(new Vector2(-15, -10));
        outcodeDL.printOutcode(outcodeDL);
        //Down
        Outcode outcodeD = new Outcode(new Vector2(0, -8));
        outcodeD.printOutcode(outcodeD);
        //Down Right
        Outcode outcodeDR = new Outcode(new Vector2(50, -3));
        outcodeDR.printOutcode(outcodeDR);



        //Trivial Acceptance (True)
        Vector2 a = new Vector2(0.5f, 0.8f);
        Vector2 b = new Vector2(0.2f, 0.2f);

        if (lineClip(ref a , ref b))
        {
            plot(breshnamsLine(Convert(a), Convert(b)));
        }

        bool trivial_accept = lineClip(ref a, ref b);
        if(trivial_accept)
        {
            print("Trivial Acceptance: " + a + b);
        }

        //Trivial Rejection
        Vector2 c = new Vector2(2, 2);
        Vector2 d = new Vector2(5, -5);
        bool trivial_reject = lineClip(ref c, ref c);
        if (!trivial_reject)
        {
            print("Trivial Rejection: " + c + d);
        }

        //Other
        Vector2 e = new Vector2(4, 0);
        Vector2 f = new Vector2(-4, 0);

        if(lineClip(ref e, ref f))
        {
            print("Accepted");
        }

        else
        {
            print("Rejected");
        }

        List<Vector2Int> list = breshnamsLine(new Vector2Int(15, 150), new Vector2Int(100, 43));

        foreach(Vector2Int v in list)
        {
            print(v);
        }
    }

    private List<Vector3> getImage(List<Vector3> list_verts, Matrix4x4 transform_matrix)
    {
        List<Vector3> hold = new List<Vector3>();

        foreach (Vector3 v in list_verts)
        {
            Vector4 v2 = new Vector4(v.x, v.y, v.z, 1);

            hold.Add(transform_matrix * v2);
        }

        return hold;
    }

    private void printVerts(List<Vector3> v_list)
    {
        string path = "Assets/Test.txt";

        StreamWriter writer = new StreamWriter(path, true);

        foreach (Vector3 v in v_list)
        {
            writer.WriteLine("(" + v.x.ToString() + ", " + v.y.ToString() + ", " + v.z.ToString() + ")");
        }

        writer.Close();
    }

    private void printMatrix(Matrix4x4 matrix)
    {
        string path = "Assets/Test.txt";

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("\n");
        for (int i = 0; i < 4; i++)
        {
            Vector4 row = matrix.GetRow(i);
            writer.WriteLine("(" + row.x.ToString() + ", " + row.y.ToString() + ", " + row.z.ToString() + ", " + row.w.ToString() + ")");
        }

        writer.Close();
    }

    private bool lineClip(ref Vector2 start, ref Vector2 end)
    {
        Outcode start_outcode = new Outcode(start);
        Outcode end_outcode = new Outcode(end);

        //Trivial Acceptance
        if((start_outcode + end_outcode) == inScreen)
        {
            return true;
        }

        //Trivial Rejection
        if((start_outcode * end_outcode) != inScreen)
        {
            return false;
        }

        if(start_outcode == inScreen)
        {
            return lineClip(ref end, ref start);
        }

        List<Vector2> intersection_points = intersectEdge(start, end, start_outcode);

        foreach (Vector2 v in intersection_points)
        {
            Outcode v_outcode = new Outcode(v);
            if (v_outcode == inScreen)
            {
                start = v;
                lineClip(ref start, ref end);
            }
        }

        return false;
    }

    private List<Vector2> intersectEdge(Vector2 start, Vector2 end, Outcode pointOutcode)
    {
        List<Vector2> intersections = new List<Vector2>();
        float m = (end.y - start.y) / (end.x - start.x);

        if(pointOutcode.up)
        {
            intersections.Add(new Vector2(start.x + 1/m * (1 - start.y) , 1));
        }

        if (pointOutcode.down)
        {
            intersections.Add(new Vector2(start.x + 1 / m * (-1 - start.y), -1));
        }

        if (pointOutcode.left)
        {
            intersections.Add(new Vector2(-1, start.y + m * (-1 - start.x)));
        }

        if (pointOutcode.right)
        {
            intersections.Add(new Vector2(1, start.y + m * (1 - start.x)));
        }

        return intersections;
    }

    private List<Vector2Int> breshnamsLine(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        double dx = end.x - start.x;

        if (dx < 0)
        {
            return breshnamsLine(end, start);
        }

        double dy = end.y - start.y;

        if (dy < 0)
        {
            return negateY(breshnamsLine(negateY(start), negateY(end)));
        }

        if (dy > dx)
        {
            return swapXY(breshnamsLine(swapXY(start), swapXY(end)));
        }

        double two_dy = 2 * dy;
        double two_dxdy = 2 * (dy - dx);
        double p = two_dy - dx;

        int y = start.y;
      
        for(int x = start.x; x <= end.x; x++)
        {
            list.Add(new Vector2Int(x, y));

            if(p <= 0)
            {
                p += two_dy; 
            }

            else
            {
                p += two_dxdy;
                y++;
            }
        }

        return list;
    }

    private List<Vector2Int> swapXY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> swapXYList = new List<Vector2Int>();

        foreach (Vector2Int v in vector2Ints)
        {
            swapXYList.Add(swapXY(v));
        }

        return swapXYList;
    }

    private Vector2Int swapXY(Vector2Int v)
    {
        return new Vector2Int(v.y, v.x);
    }

    private List<Vector2Int> negateY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> negativeYList = new List<Vector2Int>();

        foreach (Vector2Int v in vector2Ints)
        {
            negativeYList.Add(negateY(v));
        }

        return negativeYList;
    }

    private Vector2Int negateY(Vector2Int v)
    {
        return new Vector2Int(v.x, -v.y);
    }

    private void plot(List<Vector2Int> v_list)
    {
        foreach (Vector2Int v in v_list)
        {
            ourTexture.SetPixel(v.x, v.y, Color.red);
        }

        ourTexture.Apply();
    }

    private Vector2Int Convert(Vector2 v)
    {
        return new Vector2Int((int)(255 * (v.x + 1) / 2), (int)(255 * (v.y + 1) / 2));
    }
}
