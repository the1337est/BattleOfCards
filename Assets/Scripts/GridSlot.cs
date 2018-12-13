using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSlot : MonoBehaviour
{

    public Champion Champion;


    public GridRow ParentRow;
    public GridPos Position { get; private set; }

    MeshFilter mf;
    MeshRenderer rend;

    public bool IsEmpty
    {
        get
        {
            return Champion == null;
        } 
    }

    float width = 0.9f;

    [ExecuteInEditMode]
    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        Vector3[] vertices = new Vector3[4];

        float p = width / 2f;

        vertices[0] = new Vector3(-p, 0, -p);
        vertices[1] = new Vector3(p, 0, -p);
        vertices[2] = new Vector3(-p, 0f, p);
        vertices[3] = new Vector3(p, 0f, p);

        mesh.vertices = vertices;

        int[] tri = new int[6];

        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;

        mesh.triangles = tri;

       Vector3[] normals = new Vector3[4];

        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;

        mesh.normals = normals;

        Vector2[] uv = new Vector2[4];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        mesh.uv = uv;
        
    }

    [ExecuteInEditMode]
    public void Init(GridRow parent, GridPos position)
    {
        ParentRow = parent;
        Position = position;
        transform.position = new Vector3(Position.X + (3 * ((int)position.Side-1)), 0f, Position.Y);
        name = "Slot (" + position.X + ", " + position.Y + ")";

        mf = GetComponent<MeshFilter>();
        rend = GetComponent<MeshRenderer>();

        CreateMesh();
    }

    public void TakeDamage(int damage)
    {
        if (Champion != null)
        {
            Champion.Health -= damage;
            if (Champion.Health <= 0)
            {
                Clear();
            }
        }
    }

    public void Clear()
    {
        Debug.Log("Destroying " + gameObject.name);
        Destroy(Champion.gameObject);
        Champion = null;
    }

}
