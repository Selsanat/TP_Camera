using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop = false; 
    private List<Vector3> nodes = new List<Vector3>(); 
    private float length = 0; 

    void Start()
    {
        InitializeNodes();
        length = ComputeLength();
    }

    private void InitializeNodes()
    {
        nodes.Clear();
        foreach (Transform child in transform)
        {
            nodes.Add(child.position);
        }
    }

    private float ComputeLength()
    {
        float totalLength = 0;
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            totalLength += Vector3.Distance(nodes[i], nodes[i + 1]);
        }

        if (isLoop)
        {
            totalLength += Vector3.Distance(nodes[nodes.Count - 1], nodes[0]);
        }

        return totalLength;
    }

    public float GetLength()
    {
        return length;
    }

    public Vector3 GetPosition(float distance)
    {
        if (nodes.Count < 2) return nodes[0];  

        distance = Mathf.Clamp(distance, 0, length);  
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            float segmentLength = Vector3.Distance(nodes[i], nodes[i + 1]);
            if (distance <= segmentLength)
            {
                return Vector3.Lerp(nodes[i], nodes[i + 1], distance / segmentLength);
            }

            distance -= segmentLength;  
        }

        if (isLoop)
        {
            float lastSegmentLength = Vector3.Distance(nodes[nodes.Count - 1], nodes[0]);
            return Vector3.Lerp(nodes[nodes.Count - 1], nodes[0], distance / lastSegmentLength);
        }

        return nodes[nodes.Count - 1];  
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        InitializeNodes();

        for (int i = 0; i < nodes.Count - 1; i++)
        {
            Gizmos.DrawLine(nodes[i], nodes[i + 1]);
        }

        if (isLoop && nodes.Count > 1)
        {
            Gizmos.DrawLine(nodes[nodes.Count - 1], nodes[0]);
        }
    }

}
