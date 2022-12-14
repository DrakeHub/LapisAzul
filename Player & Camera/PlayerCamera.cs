using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; private set; } = null;

    [SerializeField]
    private Transform targetTransform = null;
    [SerializeField]
    private float smoothTime = 0.5f;

    private float cameraZOffset = 0f;
    private Vector3 cameraVelocity;

    private Vector3 lastOffsetPosition = Vector3.zero;
    private Coroutine lastShakeCoroutine = null;

    //Limits
    [SerializeField]
    private Transform topLimitTransform = null;
    [SerializeField]
    private Transform bottomLimitTransform = null;
    [SerializeField]
    private Transform leftLimitTransform = null;
    [SerializeField]
    private Transform rightLimitTransform = null;

    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;

    private Camera myCamera = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        cameraZOffset = transform.position.z;

        myCamera = GetComponent<Camera>();

        SetCameraLimits();
    }

    private void SetCameraLimits()
    {
        float halfHeight = myCamera.orthographicSize;
        float halfWidth = halfHeight * myCamera.aspect;

        leftLimit = leftLimitTransform.position.x + halfWidth;
        rightLimit = rightLimitTransform.position.x - halfWidth;
        topLimit = topLimitTransform.position.y - halfHeight;
        bottomLimit = bottomLimitTransform.position.y + halfHeight;
    }

    private void LateUpdate()
    {
        if (targetTransform != null)
        {
            Vector3 targetPosition = targetTransform.position;
            targetPosition.z = cameraZOffset;

            targetPosition.x = Mathf.Clamp(
                targetPosition.x, 
                leftLimit, 
                rightLimit
                );

            targetPosition.y = Mathf.Clamp(
                targetPosition.y, 
                bottomLimit, 
                topLimit
                );

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref cameraVelocity,
                smoothTime
                );
        }
    }
    public void Shake(float duration, float range)
    {
        if (lastShakeCoroutine != null)
        {
            transform.localPosition -= lastOffsetPosition;
            StopCoroutine(lastShakeCoroutine);
        }
        lastShakeCoroutine = StartCoroutine(DoShake(duration, range));
    }
    private IEnumerator DoShake(float duration, float range)
    {
        while (duration > 0f)
        {
            transform.localPosition -= lastOffsetPosition;
            lastOffsetPosition = Random.insideUnitCircle * range;
            lastOffsetPosition.z = 0f;
            transform.localPosition += lastOffsetPosition;
            duration -= Time.deltaTime;
            yield return null;
        }    
    }

    public void SetTarget(Transform newTargetTransform)
    {
        targetTransform = newTargetTransform;
    }
}
