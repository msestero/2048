using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Sprite square;
    Transform boardTF;
    GameObject[] grid;
    SpriteRenderer sr;

    bool right = true;
    bool left = true;
    bool up = true;
    bool down = true;
    void Start()
    {
        createBoard();
        createGrid();
        randomTile();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && right)
        {
            moveRight();
            right = false;
            randomTile();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && left)
        {
            moveLeft();
            left = false;
            randomTile();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && up)
        {
            moveUp();
            up = false;
            randomTile();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            up = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && down)
        {
            moveDown();
            down = false;
            randomTile();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            down = true;
        }
    }

    private void createBoard()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        boardTF = gameObject.GetComponent<Transform>();
        boardTF.position = new Vector3(0, 0, 0);
        boardTF.localScale = new Vector3(10, 10, 1);
        sr.sprite = square;
        sr.color = Color.black;
        sr.sortingOrder = 1;
    }

    private void createGrid()
    {
        int index = 0;
        grid = new GameObject[25];
        for (float x = -0.4f; x < 0.5f; x += 0.2f)
        {
            for (float y = -0.4f; y < 0.5f; y += 0.2f)
            {
                GameObject tile = new GameObject("Grid");
                Transform tileTF = tile.GetComponent<Transform>();
                tileTF.parent = boardTF;
                tileTF.localScale = new Vector3(0.19f, 0.19f, 1);
                tileTF.localPosition = new Vector3(x, y);

                SpriteRenderer tileSR = tile.AddComponent<SpriteRenderer>();
                tileSR.sprite = square;
                tileSR.color = Color.white;
                tileSR.sortingOrder = 2;
                grid[index] = tile;
                index++;
            }
        }
    }

    private bool moveDownOne(int index)
    {
        Transform tf = grid[index].GetComponent<Transform>();
        if (tf.childCount == 0)
        {
            return false;
        }
        GameObject obj = tf.GetChild(0).gameObject;
        Tile tile = obj.GetComponent<Tile>();
        if (tile.position % 5 == 0)
        {
            return false;
        }
        Transform next = grid[index - 1].GetComponent<Transform>();
        if(next.childCount > 1)
        {
            return false;
        }
        if (next.childCount == 1)
        {
            GameObject nextObj = next.GetChild(0).gameObject;
            int value = nextObj.GetComponent<Tile>().value;
            if (value == tile.value)
            {
                GameObject newObj = addTile(index - 1, 2 * value);
                Tile newTile = newObj.GetComponent<Tile>();
                tile.updatePosition(index - 1);
                newTile.setMerge(obj, nextObj);
                return true;
            }
            return false;
        }
        tile.updatePosition(index - 1);
        return true;
    }

    private bool moveDownAll()
    {
        bool ret = false;
        for (int i = 0; i < 25; i++)
        {
            if (moveDownOne(i))
            {
                ret = true;
            }
        }
        return ret;
    }

    private void moveDown()
    {
        bool val = true;
        while (val)
        {
            val = moveDownAll();
        }
    }

    private bool moveUpOne(int index)
    {
        Transform tf = grid[index].GetComponent<Transform>();
        if (tf.childCount == 0)
        {
            return false;
        }
        GameObject obj = tf.GetChild(0).gameObject;
        Tile tile = obj.GetComponent<Tile>();
        if (tile.position % 5 == 4)
        {
            return false;
        }
        Transform next = grid[index + 1].GetComponent<Transform>();
        if (next.childCount > 1)
        {
            return false;
        }
        if (next.childCount == 1)
        {
            GameObject nextObj = next.GetChild(0).gameObject;
            int value = nextObj.GetComponent<Tile>().value;
            if (value == tile.value)
            {
                GameObject newObj = addTile(index + 1, 2 * value);
                Tile newTile = newObj.GetComponent<Tile>();
                tile.updatePosition(index + 1);
                newTile.setMerge(obj, nextObj);
                return true;
            }
            return false;
        }
        tile.updatePosition(index + 1);
        return true;
    }

    private bool moveUpAll()
    {
        bool ret = false;
        for (int i = 24; i >= 0; i--)
        {
            if (moveUpOne(i))
            {
                ret = true;
            }
        }
        return ret;
    }

    private void moveUp()
    {
        bool val = true;
        while (val)
        {
            val = moveUpAll();
        }
    }

    private bool moveLeftOne(int index)
    {
        Transform tf = grid[index].GetComponent<Transform>();
        if (tf.childCount == 0)
        {
            return false;
        }
        GameObject obj = tf.GetChild(0).gameObject;
        Tile tile = obj.GetComponent<Tile>();
        if (tile.position < 5)
        {
            return false;
        }
        Transform next = grid[index - 5].GetComponent<Transform>();
        if (next.childCount > 1)
        {
            return false;
        }
        if (next.childCount == 1)
        {
            GameObject nextObj = next.GetChild(0).gameObject;
            int value = nextObj.GetComponent<Tile>().value;
            if (value == tile.value)
            {
                GameObject newObj = addTile(index - 5, 2 * value);
                Tile newTile = newObj.GetComponent<Tile>();
                tile.updatePosition(index - 5);
                newTile.setMerge(obj, nextObj);
                return true;
            }
            return false;
        }
        tile.updatePosition(index - 5);
        return true;
    }

    private bool moveLeftAll()
    {
        bool ret = false;
        for (int i = 0; i < 25; i++)
        {
            if (moveLeftOne(i))
            {
                ret = true;
            }
        }
        return ret;
    }

    private void moveLeft()
    {
        bool val = true;
        while (val)
        {
            val = moveLeftAll();
        }
    }
    private bool moveRightOne(int index)
    {
        Transform tf = grid[index].GetComponent<Transform>();
        if (tf.childCount == 0)
        {
            return false;
        }
        GameObject obj = tf.GetChild(0).gameObject;
        Tile tile = obj.GetComponent<Tile>();
        if (tile.position >= 20)
        {
            return false;
        }
        Transform next = grid[index + 5].GetComponent<Transform>();
        if (next.childCount > 1)
        {
            return false;
        }
        if (next.childCount == 1)
        {
            GameObject nextObj = next.GetChild(0).gameObject;
            int value = nextObj.GetComponent<Tile>().value;
            if (value == tile.value)
            {
                GameObject newObj = addTile(index + 5, 2 * value);
                Tile newTile = newObj.GetComponent<Tile>();
                tile.updatePosition(index + 5);
                newTile.setMerge(obj, nextObj);
                return true;
            }
            return false;
        }
        tile.updatePosition(index + 5);
        return true;
    }

    private bool moveRightAll()
    {
        bool ret = false;
        for (int i = 24; i >= 0; i--)
        {
            if (moveRightOne(i))
            {
                ret = true;
            }
        }
        return ret;
    }

    private void moveRight()
    {
        bool val = true;
        while (val)
        {
            val = moveRightAll();
        }
    }

    private void randomTile()
    {
        int position = Random.Range(0, 24);
        while (grid[position].transform.childCount > 0)
        {
            position = Random.Range(0, 24);
        }
        addTile(position, 2 * Random.Range(1, 2));
    }

    private GameObject addTile(int position, int value)
    {
        GameObject newTile = new GameObject("Tile");
        Tile tileScript = newTile.AddComponent<Tile>();
        tileScript.board = gameObject;
        tileScript.sprite = square;
        tileScript.position = position;
        tileScript.value = value;
        return newTile;
    }

    public GameObject[] getGrid()
    {
        return grid;
    }
}
