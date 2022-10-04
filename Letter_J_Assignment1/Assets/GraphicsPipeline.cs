using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    void Start()
    {
        Model my_Model = new Model();
        my_Model.CreateUnityGameObject();

        List<Vector3> verts = my_Model.vertices;
        print_verts(verts);

        //Rotation Matrix
        Vector3 axis = new Vector3(12, 3, 3);
        axis.Normalize();
        Matrix4x4 rotation_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(12, axis), Vector3.one);
        print_matrix(rotation_matrix);

        //Print New Vertices after Roation
        List<Vector3> image_after_rotation = get_image(verts, rotation_matrix);
        print_verts(image_after_rotation);

        //Scale Matrix
        Vector3 scale_by = new Vector3(4, 2, 3);
        Matrix4x4 scale_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale_by);
        print_matrix(scale_matrix);

        //Print New Vertices after Scale
        List<Vector3> image_after_scale = get_image(image_after_rotation, scale_matrix);
        print_verts(image_after_scale);

        //Translate Matrix
        Vector3 translate_by = new Vector3(-3, 1, 4);
        Matrix4x4 translate_matrix = Matrix4x4.TRS(translate_by, Quaternion.identity, Vector3.one);
        print_matrix(translate_matrix);

        //Print New Vertices after Translation
        List<Vector3> image_after_translation = get_image(image_after_scale, translate_matrix);
        print_verts(image_after_translation);

        //Print Single Matrix
        Matrix4x4 single_matrix = translate_matrix * scale_matrix * rotation_matrix;
        print_matrix(single_matrix);

        //Print Vertices using the one matrix
        List<Vector3> image_after_single_matrix = get_image(verts, single_matrix);
        print_verts(image_after_single_matrix);
    }

    private List<Vector3> get_image(List<Vector3> list_verts, Matrix4x4 transform_matrix)
    {
        List<Vector3> hold = new List<Vector3>();

        foreach (Vector3 v in list_verts)
        {
            hold.Add(transform_matrix * v);
        }

        return hold;
    }

    private void print_verts(List<Vector3> v_list)
    {
        string path = "Assets/test.txt";

        StreamWriter writer = new StreamWriter(path, true);

        foreach (Vector3 v in v_list)
        {
            writer.WriteLine("(" + v.x.ToString() + ", " + v.y.ToString() + ", " + v.z.ToString() + ")");
        }

        writer.Close();
    }

    private void print_matrix(Matrix4x4 matrix)
    {
        string path = "Assets/test.txt";

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("\n");
        for (int i = 0; i < 4; i++)
        {
            Vector4 row = matrix.GetRow(i);
            writer.WriteLine("(" + row.x.ToString() + ", " + row.y.ToString() + ", " + row.z.ToString() + ", " + row.w.ToString() + ")");
        }

        writer.Close();
    }
}
