using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;  // Détermine si le rail boucle
    private List<Vector3> nodes = new List<Vector3>();  // Liste des nœuds du rail
    private float length = 0;  // Longueur totale du rail

    void Start()
    {
        InitializeNodes();
        length = ComputeLength();
    }

    // Récupère les positions des enfants pour définir les nœuds du rail
    private void InitializeNodes()
    {
        nodes.Clear();
        foreach (Transform child in transform)
        {
            nodes.Add(child.position);
        }
    }

    // Calcule la longueur totale du rail
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

    // Renvoie la longueur du rail
    public float GetLength()
    {
        return length;
    }

    // Renvoie la position sur le rail à une distance donnée
    public Vector3 GetPosition(float distance)
    {
        if (nodes.Count < 2) return nodes[0];  // Cas où il n'y a pas assez de nœuds

        distance = Mathf.Clamp(distance, 0, length);  // On s'assure que la distance est dans les bornes

        for (int i = 0; i < nodes.Count - 1; i++)
        {
            float segmentLength = Vector3.Distance(nodes[i], nodes[i + 1]);
            if (distance <= segmentLength)
            {
                // Interpolation linéaire entre les nœuds
                return Vector3.Lerp(nodes[i], nodes[i + 1], distance / segmentLength);
            }

            distance -= segmentLength;  // Réduire la distance par la longueur du segment
        }

        if (isLoop)
        {
            // Si le rail est une boucle, on connecte le dernier point au premier
            float lastSegmentLength = Vector3.Distance(nodes[nodes.Count - 1], nodes[0]);
            return Vector3.Lerp(nodes[nodes.Count - 1], nodes[0], distance / lastSegmentLength);
        }

        return nodes[nodes.Count - 1];  // Si on dépasse la distance totale, on renvoie le dernier point
    }

    // Dessiner les Gizmos pour visualiser le rail dans l'éditeur
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
