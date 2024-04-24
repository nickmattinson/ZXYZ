using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public Transform circlePrefab;
    private Transform circleInstance;

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the circle is on the same Z-axis as the Player

        float distance = Vector3.Distance(transform.position, mousePosition);


        // if (circleInstance == null)
        // {
        //     circleInstance = Instantiate(circlePrefab, mousePosition, Quaternion.identity);
        // }
        // else
        // {
        //     circleInstance.position = mousePosition;
        // }

        if (distance < 3f)
        {
            //circleInstance.GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log($"Green: {distance}");
        }
        else if (distance < 5f)
        {
            //circleInstance.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log($"Blue: {distance}");
        }
        else
        {
            //circleInstance.GetComponent<SpriteRenderer>().color = Color.black;
            Debug.Log($"Black: {distance}");
        }
    }
}
