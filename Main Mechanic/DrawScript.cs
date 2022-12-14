using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawScript : MonoBehaviour
{
    // Camera
    [SerializeField] private Camera mainCamera;

    // Objects
    [SerializeField]
    private GameObject pencilOn;
    [SerializeField]
    private GameObject eraserOn;
    [SerializeField]
    private GameObject brush;
    private Animator myAnimator = null;

    // Inputs
    private InputActions inputActions = null;
    
    // Mouse & Paint
    private Vector2 mousePosition = new Vector2();
    private Vector2 lastPosition = new Vector2();
    private Vector2 pencilTipPosition = new Vector2(0,57);
    private Vector2 eraserTipPosition = new Vector2(3, 80);
    private LineRenderer currentLineRenderer;
    [SerializeField] 
    private float mouseBrushZ;
    [SerializeField]
    private Texture2D pencilTip;
    [SerializeField]
    private Texture2D eraserTip;

    // Platform
    [SerializeField]
    private LayerMask layerPoint;
    private Collider2D[] results = new Collider2D[1];
    private List <Collider2D> points = new List<Collider2D>();
    private GameObject platform = null;
    private GameObject groundPlatform = null;

    // Booleans
    private bool isPainting = false;
    private bool isErasing = false;
    private bool firstTry = true;

    private void Awake()
    {
        inputActions = new InputActions();
        pencilOn.SetActive(false);
        eraserOn.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        if (isErasing && eraserOn.activeInHierarchy && !isPainting)
        {
            MousePosition();
            Cursor.SetCursor(eraserTip, eraserTipPosition, CursorMode.ForceSoftware);
            eraserOn.transform.position = mousePosition;
        }
        if (isPainting && pencilOn.activeInHierarchy && !isErasing) // while mouse is being held down
        {
            MousePosition();
            Cursor.SetCursor(pencilTip, pencilTipPosition, CursorMode.ForceSoftware);
            pencilOn.transform.position = mousePosition;
            if (mousePosition != lastPosition)
            {
                AddPoint(mousePosition);
                lastPosition = mousePosition;
            }
        }
        else 
        {
            currentLineRenderer = null;
            StopAllCoroutines();
        }
    }
    private IEnumerator StartSearch() // Procura os pontos da plataforma
    {
        while (true)
        {
            if (Physics2D.OverlapPointNonAlloc(MousePosition(), results, layerPoint) > 0)
            {
                Point result = results[0].GetComponent<Point>();

                if (firstTry == true && result.GetVisited() == false && result.GetFirst() == false) // Se for o primeiro ponto
                {
                    result.SetVisited(true);
                    result.SetFirst(true);
                    points.Add(results[0]);
                    results[0].enabled = false;
                    firstTry = false;
                }
                if (result.GetVisited() == false) // Se for outro ponto a seguir ao primeiro
                {
                    result.SetVisited(true);
                    points.Add(results[0]);
                    results[0].enabled = false;
                    if (points.Count == 4) // Quando todos os vertices são percorridos, liga de novo o primeiro
                    {
                        points[0].enabled = true;
                    }
                }
                if (points.Count == 4 && result.GetFirst() == true) // Se já tiver percorrido todos os pontos e tocar de volta no primeiro ponto
                {
                    platform.SetActive(false);
                    groundPlatform.SetActive(true);
                    StopAllCoroutines();
                    if (GameObject.Find("Line(Clone)") != null)
                    {
                        FindObjectOfType<LineDisappear>().DestroyNow();
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void OnPaint() // Verifica se o rato está pressionado 
    {
        this.isPainting = !this.isPainting;

        if (isPainting) // Quando Left Mouse está a ser pressionado
        {
            eraserOn.SetActive(false);
            pencilOn.SetActive(true);
            StartCoroutine("StartSearch");
            CreateBrush();
            myAnimator.ResetTrigger("isTriggered");
        }
        else // Quando Left Mouse pára de ser pressionado
        {
            if (myAnimator != null)
            {
                myAnimator.SetTrigger("isTriggered");
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            pencilOn.SetActive(false);
            for (int i = 0; i < points.Count; i++)
            {
                points[i].enabled = true;
                points[i].GetComponent<Point>().SetVisited(false);
                points[i].GetComponent<Point>().SetFirst(false);
            }
            firstTry = true;
            points.Clear();
            platform = null;
            groundPlatform = null;
        }
    }

    public void OnErase()
    {
        this.isErasing = !this.isErasing;

        if (isErasing) // Quando Right Mouse está a ser pressionado
        {
            eraserOn.SetActive(true);
            pencilOn.SetActive(false);
        }
        else // Quando Right Mouse pára de ser pressionado
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            eraserOn.SetActive(false);
        }
    }

    private void CreateBrush() // Cria os primeiros pontos da linha
    {
        GameObject brushInstance = Instantiate(brush);
        myAnimator = brushInstance.GetComponent<Animator>();
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        MousePosition();

        currentLineRenderer.SetPosition(0, mousePosition);
        currentLineRenderer.SetPosition(1, mousePosition);
    }

    private void AddPoint(Vector3 pointPos) // Adiciona um novo ponto na linha onde terminou a anterior
    {
        if (currentLineRenderer == null)
        {
            pencilOn.SetActive(false);
        }
        else
        {
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount - 1;
            currentLineRenderer.SetPosition(positionIndex, pointPos);
        }
    }

    private Vector3 MousePosition() // Devolve a posição do rato relativamente à câmara
    {
        return mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public GameObject GetLine()
    {
        return brush;
    }

    public void GetGameObjects(GameObject PlatformChild, GameObject groundPlatformChild)
    {
        platform = PlatformChild;
        groundPlatform = groundPlatformChild;
    }

}