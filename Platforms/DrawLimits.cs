using UnityEngine;

public class DrawLimits : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Paint"))
        {
            if (GameObject.Find("Line(Clone)") != null)
            {
                FindObjectOfType<LineDisappear>().DestroyNow();
                //col.GetComponent<DrawScript>().GetLine().GetComponent<LineDisappear>()?.DestroyNow(); Stopped working, not sure why
            }
        }
    }
}
