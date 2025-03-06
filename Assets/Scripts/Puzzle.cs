using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.UI;
using UnityEngine;

enum Connection {EMPTY, HOLE, KNOB};

public class Puzzle : MonoBehaviour
{
    const int LEFT = 0;
    const int RIGHT = 1;
    const int UP = 2;
    const int DOWN = 3;

    Connection[] connections;

    [SerializeField] private Connection left;
    [SerializeField] private Connection right;
    [SerializeField] private Connection up;
    [SerializeField] private Connection down;

    [SerializeField] public bool portable;
    [SerializeField] private bool alwasVisible;

    private Grid grid;
    private int gridSizeX;
    private int gridSizeY;
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 puzzlePosition;
    private GameObject[] children;

    public bool isPlaced = true;

    void Start()
    {
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        player = GameObject.FindWithTag("Player");

        gridSizeX = grid.GetGridSizeX();
        gridSizeY = grid.GetGridSizeY();

        children = new GameObject[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            children[i] = this.transform.GetChild(i).gameObject;
        }

        connections = new Connection[] { left, right, up, down };
    }

    void FixedUpdate()
    {
        puzzlePosition = this.transform.position;
        playerPosition = player.transform.position;

        bool isNearby = Mathf.Abs(playerPosition.y - puzzlePosition.y) < 0.1f && Mathf.Abs(playerPosition.x - puzzlePosition.x) <= 1.0f;

        if ((isNearby || alwasVisible) && isPlaced)
        {
            foreach (GameObject child in children)
                child.SetActive(true);
        }
        else
        {
            foreach (GameObject child in children)
                child.SetActive(false);
        }
            
    }

    public bool FitsAt(int x, int y)
    {
        Puzzle neighbour;
        //left
        if(x >= 1)
        {
            neighbour = grid.GetPuzzleFromGrid(x - 1, y);
            if (neighbour != null)
            {
                if (!canConnect(connections[LEFT], neighbour.connections[RIGHT]))
                    return false;
            }
        }

        //right
        if (x <= gridSizeX - 2)
        {
            neighbour = grid.GetPuzzleFromGrid(x + 1, y);
            if (neighbour != null)
            {
                if (!canConnect(connections[RIGHT], neighbour.connections[LEFT]))
                    return false;
            }
        }

        //up
        if (y <= gridSizeX - 2)
        {
            neighbour = grid.GetPuzzleFromGrid(x, y + 1);
            if (neighbour != null)
            {
                if (!canConnect(connections[UP], neighbour.connections[DOWN]))
                    return false;
            }
        }

        //down
        if (y >= 1)
        {
            neighbour = grid.GetPuzzleFromGrid(x, y - 1);
            if (neighbour != null)
            {
                if (!canConnect(connections[DOWN], neighbour.connections[UP]))
                    return false;
            }
        }

        return true;
    }

    static bool canConnect(Connection con1, Connection con2)
    {
        if (con1 == Connection.KNOB && con2 == Connection.HOLE)
            return true;
        if (con1 == Connection.HOLE && con2 == Connection.KNOB)
            return true;
        if (con1 == Connection.EMPTY && con2 == Connection.EMPTY)
            return true;

        return false;
    }
}