using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] points = null;

    private Vector3 startPosition;

    private Vector3 targetPosition;

    private int targetIndex = 1;

    private float startTime = 0f;

    [SerializeField]
    private float duration = 3f;

    private void Awake()
    {
        startPosition = points[0].position;
        targetPosition = points[targetIndex].position;
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        float t = (Time.time - startTime) / duration;

        transform.position = new Vector3(
            Mathf.SmoothStep(startPosition.x, targetPosition.x, t),
            Mathf.SmoothStep(startPosition.y, targetPosition.y, t),
            transform.position.z
            );

        if (t >= 1f)
        {
            MoveToNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        startPosition = targetPosition;
        /*targetIndex++;
        if (targetIndex == points.Length)
        {
            targetIndex = 0;
        }*/
        targetIndex = (targetIndex + 1) % points.Length;
        targetPosition = points[targetIndex].position;
        startTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Length; ++i)
        {
            Gizmos.DrawWireSphere(points[i].position, 0.3f);
            Gizmos.DrawLine(points[i].position, points[(i + 1) % points.Length].position);
        }
    }
}
