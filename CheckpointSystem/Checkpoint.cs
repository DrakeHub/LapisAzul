using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool checkIsActive = true;
    private Vector3 coordinates = new Vector3();
    Collider2D collider = null;
    [SerializeField]
    private float yMargin = 0.52f;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public Vector3 GetPosition()
    {
        if (checkIsActive)
        {
            checkIsActive = false;
            coordinates = this.gameObject.transform.position;
            coordinates.y = collider.bounds.min.y + yMargin;
            coordinates.x = collider.bounds.center.x;
            collider.enabled = false;
            return coordinates;
        }
        return Vector3.zero;
    }
}
