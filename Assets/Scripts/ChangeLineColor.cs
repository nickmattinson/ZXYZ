using UnityEngine;

public class ChangeLineColor : MonoBehaviour
{
    private Entity entity; // Reference to the Player class

    private LineRenderer lineRenderer; // Reference to the line renderer component

    private void Start()
    {
        // Get the Player component attached to the GameObject
        entity = GetComponent<Entity>();

        // Get the SpriteRenderer component attached to the GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Check if the Player component and sprite renderer component exist
        if (entity != null && lineRenderer != null)
        {
            // Set the sprite's color to the spriteColor attribute from the Player class
            Color customColor = entity.GetAttackColor();
            lineRenderer.material.SetColor("_Color", customColor);
        }
        else
        {
            Debug.LogError("Entity or LineRenderer component not found!");
        }
    }
}

