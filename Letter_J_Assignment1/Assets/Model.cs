using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    internal List<Vector3> vertices = new List<Vector3>();
    List<Vector3Int> faces = new List<Vector3Int>();
    /*List<Vector2> texture_coordinates = new List<Vector2>();
    List<Vector3Int> texture_index_list = new List<Vector3Int>();
    List<Vector3> normals = new List<Vector3>();*/

    public Model()
    {
        addVertices();
        addFaces();
    }

    private void addFaces()
    {
        //Front
        faces.Add(new Vector3Int(1, 0, 2));
        faces.Add(new Vector3Int(1, 2, 5));
        faces.Add(new Vector3Int(4, 3, 10));
        faces.Add(new Vector3Int(4, 10, 11));
        faces.Add(new Vector3Int(10, 13, 11));
        faces.Add(new Vector3Int(10, 9, 12));
        faces.Add(new Vector3Int(10, 12, 13));
        faces.Add(new Vector3Int(9, 8, 12));
        faces.Add(new Vector3Int(7, 8, 9));
        faces.Add(new Vector3Int(7, 6, 8));
        
        //Back
        faces.Add(new Vector3Int(15, 16, 14));
        faces.Add(new Vector3Int(15, 19, 16));
        faces.Add(new Vector3Int(18, 24, 17));
        faces.Add(new Vector3Int(18, 25, 24));
        faces.Add(new Vector3Int(24, 25, 27));
        faces.Add(new Vector3Int(24, 27, 26));
        faces.Add(new Vector3Int(24, 26, 23));
        faces.Add(new Vector3Int(23, 26, 22));
        faces.Add(new Vector3Int(21, 23, 22));
        faces.Add(new Vector3Int(21, 22, 20));

        //Left
        faces.Add(new Vector3Int(0, 14, 16));
        faces.Add(new Vector3Int(0, 16, 2));
        faces.Add(new Vector3Int(3, 17, 24));
        faces.Add(new Vector3Int(3, 24, 10));
        faces.Add(new Vector3Int(6, 20, 22));
        faces.Add(new Vector3Int(6, 22, 8));
        faces.Add(new Vector3Int(22, 26, 12));
        faces.Add(new Vector3Int(22, 12, 8));

        //Right
        faces.Add(new Vector3Int(15, 1, 5));
        faces.Add(new Vector3Int(15, 5, 19));
        faces.Add(new Vector3Int(18, 4, 11));
        faces.Add(new Vector3Int(18, 11, 25));
        faces.Add(new Vector3Int(21, 7, 9));
        faces.Add(new Vector3Int(21, 9, 23));
        faces.Add(new Vector3Int(11, 13, 27));
        faces.Add(new Vector3Int(11, 27, 25));

        //Top
        faces.Add(new Vector3Int(15, 14, 0));
        faces.Add(new Vector3Int(15, 0, 1));
        faces.Add(new Vector3Int(21, 20, 6));
        faces.Add(new Vector3Int(21, 6, 7));
        faces.Add(new Vector3Int(23, 9, 10));
        faces.Add(new Vector3Int(23, 10, 24));

        //Bottom
        faces.Add(new Vector3Int(8, 22, 26));
        faces.Add(new Vector3Int(8, 26, 12));
        faces.Add(new Vector3Int(13, 12, 26));
        faces.Add(new Vector3Int(13, 26, 27));
        faces.Add(new Vector3Int(13, 27, 25));
        faces.Add(new Vector3Int(13, 25, 11));
        faces.Add(new Vector3Int(4, 18, 19));
        faces.Add(new Vector3Int(4, 19, 5));
        faces.Add(new Vector3Int(3, 2, 16));
        faces.Add(new Vector3Int(3, 16, 17));
    }

    private void addVertices()
    {
        vertices.Add(new Vector3(-5, 5, 1));
        vertices.Add(new Vector3(5, 5, 1));
        vertices.Add(new Vector3(-5, 3, 1));
        vertices.Add(new Vector3(-1, 3, 1));
        vertices.Add(new Vector3(1, 3, 1));
        vertices.Add(new Vector3(5, 3, 1));
        vertices.Add(new Vector3(-5, -1, 1));
        vertices.Add(new Vector3(-3, -1, 1));
        vertices.Add(new Vector3(-5, -3, 1));
        vertices.Add(new Vector3(-3, -3, 1));
        vertices.Add(new Vector3(-1, -3, 1));
        vertices.Add(new Vector3(1, -3, 1));
        vertices.Add(new Vector3(-3, -5, 1));
        vertices.Add(new Vector3(-1, -5, 1));
        vertices.Add(new Vector3(-5, 5, -1));
        vertices.Add(new Vector3(5, 5, -1));
        vertices.Add(new Vector3(-5, 3, -1));
        vertices.Add(new Vector3(-1, 3, -1));
        vertices.Add(new Vector3(1, 3, -1));
        vertices.Add(new Vector3(5, 3, -1));
        vertices.Add(new Vector3(-5, -1, -1));
        vertices.Add(new Vector3(-3, -1, -1));
        vertices.Add(new Vector3(-5, -3, -1));
        vertices.Add(new Vector3(-3, -3, -1));
        vertices.Add(new Vector3(-1, -3, -1));
        vertices.Add(new Vector3(1, -3, -1));
        vertices.Add(new Vector3(-3, -5, -1));
        vertices.Add(new Vector3(-1, -5, -1));
    }

    public GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();
     
        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        /*List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();*/

        for (int i = 0; i < faces.Count; i++)
        {
            //Vector3 normal_for_face = normals[i];

            //normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); //text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); //text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); //text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        /*mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();*/
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
