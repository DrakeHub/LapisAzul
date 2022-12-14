using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject groundPlatform;
    [SerializeField]
    private float timeToRestart = 10f;
    private void OnEnable()
    {
        Invoke("Restart", timeToRestart);
    }

    void Restart()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        platform.SetActive(true);
        groundPlatform.SetActive(false);
    }
}