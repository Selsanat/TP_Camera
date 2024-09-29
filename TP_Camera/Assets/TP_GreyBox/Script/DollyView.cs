using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
    public float roll;  // Roulement de la caméra
    public float fov;   // Champ de vision de la caméra
    public Transform target;  // Cible à suivre
    public Rail rail;  // Rail auquel est attachée la caméra
    public float distanceOnRail = 0f;  // Distance actuelle sur le rail
    public float speed = 5f;  // Vitesse de déplacement sur le rail

    // Surcharge de la méthode GetConfiguration pour gérer le mouvement le long du rail
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();

        if (rail == null)
        {
            Debug.LogWarning("Rail non défini pour DollyView.");
            return config;
        }

        // Calcul de la position sur le rail en fonction de la distance
        Vector3 railPosition = rail.GetPosition(distanceOnRail);

        // Calcul de la direction vers la cible
        if (target != null)
        {
            Vector3 dir = (target.position - railPosition).normalized;

            // Calcul des angles yaw et pitch pour suivre la cible
            config.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            config.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        }

        // Paramètres de la configuration de la caméra
        config.roll = roll;
        config.fieldOfView = fov;
        config.pivot = railPosition;  // Position sur le rail
        config.distance = 0;  // Distance à 0 car la caméra est sur le rail

        return config;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");  // Récupère l'input horizontal (clavier ou manette)
        distanceOnRail += input * speed * Time.deltaTime;  // Déplacement en fonction de la vitesse

        // S'assurer que la distance reste dans les limites du rail
        if (rail.isLoop)
        {
            distanceOnRail = Mathf.Repeat(distanceOnRail, rail.GetLength());
        }
        else
        {
            distanceOnRail = Mathf.Clamp(distanceOnRail, 0, rail.GetLength());
        }
    }
}
