using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool first = false;
    private bool visited = false;

    public bool GetFirst()
    {
        return first;
    }
    public void SetFirst(bool first) // quando for o primeiro
    {
        this.first = first;
    }
    public bool GetVisited()
    {
        return visited;
    }
    public void SetVisited(bool visited) // quando for o primeiro
    {
        this.visited = visited;
    }
}
