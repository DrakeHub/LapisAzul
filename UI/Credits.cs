using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : ButtonManager
{
    private bool isPlayerInside = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            ShowCredits();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlayerInside)
        {
            isPlayerInside = false;
        }
    }
}
