using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlatform : MonoBehaviour
{
    [SerializeField]
    private DrawScript drawScript;
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject groundPlatform;
    private void Awake()
    {
        groundPlatform.SetActive(false);
        drawScript = FindObjectOfType<DrawScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Paint"))
        {
            drawScript.GetGameObjects(platform, groundPlatform); //Envia os gameObjects para o DrawScript para saber quais ativar e desativar
        }
    }
}
