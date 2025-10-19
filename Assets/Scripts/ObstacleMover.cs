using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleMover : MonoBehaviour
{
    public static float baseSpeed = 0.1f;
    public float currentSpeed;
    public float ObstacleSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = baseSpeed;
        if (transform.position.x > 0)
        {
            ObstacleSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(ObstacleSpeed * currentSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x > 18 || transform.position.x < -18)
        {
            Destroy(gameObject);
        }
    }
}
