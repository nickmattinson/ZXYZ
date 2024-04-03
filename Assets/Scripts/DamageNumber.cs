using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] float floatSpeed = 1f; // Speed at which the damage number floats up
    //[SerializeField] float fadeSpeed = 1f; // Speed at which the damage number fades out
    [SerializeField] float lifeTime = 1f; // Time before the damage number disappears
    private float timer = 0f;
    public TextMeshPro damageText;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the damage number upwards
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // Fade out the damage number
        float alpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
        //damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, alpha);
        
        // Update timer
        timer += Time.deltaTime;
    }

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
    }
}
