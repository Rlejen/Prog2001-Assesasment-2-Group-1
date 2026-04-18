using UnityEngine;

public class FloatSnowball : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.localPosition;
        randomOffset = Random.Range(0f, Mathf.PI * 2f); // desync floating
    }

    void Update()
    {
        // Rotate
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        // Float up and down (only Y axis affected)
        float newY = Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatHeight;
        transform.localPosition = startPos + new Vector3(0, newY, 0);
    }
}