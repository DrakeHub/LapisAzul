using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private Dictionary<GameObject, Transform> oldParents = 
                        new Dictionary<GameObject, Transform>();

    /*private List<Transform> oldParents = new List<Transform>();
    private List<GameObject> parentedObjects = new List<GameObject>();
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        oldParents.Add(other.gameObject, other.transform.parent);

        /*oldParents.Add(other.transform.parent);
        parentedObjects.Add(other.gameObject);*/
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (oldParents.TryGetValue(other.gameObject, out Transform oldParent))
        {
            other.transform.SetParent(oldParent);
            oldParents.Remove(other.gameObject);
        }
        else
        {
            other.transform.SetParent(null);
        }

        /*for (int i=0; i < parentedObjects.Count; ++i)
        {
            if (other.gameObject == parentedObjects[i])
            {
                other.transform.SetParent(oldParents[i]);
                oldParents.RemoveAt(i);
                parentedObjects.RemoveAt(i);
                break;
            }
        }*/
    }
}
