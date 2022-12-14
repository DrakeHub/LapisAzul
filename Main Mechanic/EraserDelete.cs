using UnityEngine;

public class EraserDelete : MonoBehaviour
{
    [SerializeField]
    private GameObject parent = null;
    private SpriteRenderer[] colorFade = null;
    private int c = 0;

    private void Awake()
    {
        colorFade = parent.GetComponentsInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Eraser"))
        {
            for (int i = 0; i < colorFade.Length; i++)
            {
                Color fade = colorFade[i].color;
                fade.a -= 0.33f;
                colorFade[i].color = fade;
            }
            c++;
            if (c != 5)
            {
                c++;
            }
            else
            {
                Destroy(parent);
            }
        }
    }
}
