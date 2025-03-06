using UnityEngine;

public class PuzzleDrag : MonoBehaviour
{
    private GameObject selectedPiece;
    private GameObject pieceImage;

    private Vector3 offset;
    private float zPosition = 0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private SoundsManager soundManager;
    private Puzzle puzzle;
    private Grid grid;
    private GameObject player;

    private int startX;
    private int startY;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundsManager>();
        grid = FindObjectOfType<Grid>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        //Left Button
        if (Input.GetMouseButtonDown(0))
        {
            //Mouse Position To World Position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Collision Check
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Piece"))
            {
                selectedPiece = hit.collider.gameObject;
                offset = selectedPiece.transform.position - hit.point;

                startPosition = selectedPiece.transform.position;

                //Grid Position
                startX = Mathf.RoundToInt(startPosition.x / grid.cellSize);
                startY = Mathf.RoundToInt(startPosition.y / grid.cellSize);

                //Get the puzzle and remove it from the grid for now
                puzzle = grid.GetPuzzleFromGrid(startX, startY);
                grid.SetPuzzle(null, startX, startY);
                puzzle.isPlaced = false;

                if ((puzzle.portable && (int)player.transform.position.x != (int)puzzle.transform.position.x)
                    || (puzzle.portable && (int)player.transform.position.y != (int)puzzle.transform.position.y))
                {
                    soundManager.PlaySound(SoundsManager.Sounds.TakePuzzle);
                    //Select Piece Image
                    pieceImage = selectedPiece.transform.GetChild(0).gameObject;

                    //Higher layer
                    SetOrderInLayer(selectedPiece, 1);
                    SetOrderInLayer(pieceImage, 1);
                }
                else
                {
                    soundManager.PlaySound(SoundsManager.Sounds.Error);
                    selectedPiece.transform.position = startPosition;
                    puzzle.isPlaced = true;
                    grid.SetPuzzle(selectedPiece.GetComponent<Puzzle>(), startX, startY);
                    selectedPiece = null;
                }
            }
        }

        //Move Piece
        if (selectedPiece != null )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition)); 
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 mousePosition = ray.GetPoint(distance);
                selectedPiece.transform.position = mousePosition + offset;
            }
        }

        //Put Piece
        if (Input.GetMouseButtonUp(0) && selectedPiece != null )
        {
            soundManager.PlaySound(SoundsManager.Sounds.PutPuzzle); 
            endPosition = selectedPiece.transform.position;

            //Grid Position
            int endX = Mathf.RoundToInt(endPosition.x / grid.cellSize);
            int endY = Mathf.RoundToInt(endPosition.y / grid.cellSize);

            //Check if already occupied
            if (grid.GetPuzzleFromGrid(endX, endY) != null)
            {
                selectedPiece.transform.position = startPosition;
                grid.SetPuzzle(selectedPiece.GetComponent<Puzzle>(), startX, startY);
            }

            //Check if the puzzle doesn't fit
            else if(!selectedPiece.GetComponent<Puzzle>().FitsAt(endX, endY))
            {
                selectedPiece.transform.position = startPosition;
                grid.SetPuzzle(selectedPiece.GetComponent<Puzzle>(), startX, startY);
            }

            //Success
            else
            {
                grid.SetPuzzle(puzzle, endX, endY);
                selectedPiece.transform.position = new Vector3(endX * grid.cellSize, endY * grid.cellSize, zPosition);
            }

            //Lower layer
            SetOrderInLayer(selectedPiece, 0);
            SetOrderInLayer(pieceImage, 0);
            puzzle.isPlaced = true;
            selectedPiece = null;
            pieceImage = null;
        }
    }

    private void SetOrderInLayer(GameObject piece, int order)
    {
        var spriteRenderer = piece.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = order;
        }
    }
}