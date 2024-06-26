using UnityEngine;

public class ChangeSpriteColor : MonoBehaviour
{
    private Entity entity; // Reference to the Player class

    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer component

    private void Start()
    {
        // Get the Player component attached to the GameObject
        entity = GetComponent<Entity>();

        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the Player component and sprite renderer component exist
        if (entity != null && spriteRenderer != null)
        {
            // Set the sprite's color to the spriteColor attribute from the Player class

            // Create a new RGBA color using the Color constructor and store it in a variable
            Color customColor = entity.GetSpriteColor();
            spriteRenderer.material.SetColor("_Color", customColor);
        }
        else
        {
            Debug.LogError("Entity or SpriteRenderer component not found!");
        }
    }
}

