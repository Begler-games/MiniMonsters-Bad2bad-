using UnityEngine;

public class DistanceCircle : MonoBehaviour
{
    public float detectionRadius = 3f;          // Circle radius size
    public int segments = 50;                   // Number of points in the circle
    public LineRenderer lineRenderer;           // LineRenderer component for drawing the circle

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // Simple material for visibility

        DrawCircle();
    }

    void Update()
    {
        DrawCircle();  // Keep circle updated in case of size changes
    }

    void DrawCircle()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * detectionRadius;
            float y = Mathf.Sin(angle) * detectionRadius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            angle += 2 * Mathf.PI / segments;
        }
    }
}
