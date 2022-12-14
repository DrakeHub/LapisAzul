using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //Sim, este script é cheating, eu podia ter feito um Checkpoint Manager que não era destruido onLoad,
    //ou até ter feito isto no Game Manager que dizia ao script do jogador onde ele ia fazer spawn ao enviar as coordenadas via método público para ele.
    //Mas decidi fazer desta forma :D

    private Vector3 currentCheckpoint = new Vector3(-1 ,0 ,1);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.GetComponent<Checkpoint>().GetPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            this.gameObject.transform.position = currentCheckpoint;
        }
    }
    
}
