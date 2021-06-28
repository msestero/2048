using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour
{
    public int position;
    public int value;
    public GameObject board;
    public Sprite sprite;

    private GameObject obj1 = null;
    private GameObject obj2 = null;
    SpriteRenderer sr;
    Transform tf;

    void Start()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        tf = GetComponent<Transform>();
        tf.parent = board.GetComponent<Board>().getGrid()[position].GetComponent<Transform>();
        tf.localPosition = Vector3.zero;
        tf.localScale = new Vector3(0.9f, 0.9f, 1);
        getColor(value);
        sr.sortingOrder = 3;
        addText();
    }

    private void getColor(int value)
    {
        switch (value)
        {
            case 2:
                sr.color = new Color(1, 0.4f, 0.4f);
                break;
            case 4:
                sr.color = new Color(1, 0, 0);
                break;
            case 8:
                sr.color = new Color(0.6f, 0, 0);
                break;
            case 16:
                sr.color = new Color(1, 1, 0.4f);
                break;
            case 32:
                sr.color = new Color(1, 1, 0);
                break;
            case 64:
                sr.color = new Color(0.6f, 0.6f, 0);
                break;
            case 128:
                sr.color = new Color(0.4f, 1, 0.4f);
                break;
            case 256:
                sr.color = new Color(0, 1, 0);
                break;
            case 512:
                sr.color = new Color(0, 0.6f, 0);
                break;
            case 1024:
                sr.color = new Color(0.4f, 0.4f, 1);
                break;
            case 2048:
                sr.color = new Color(0, 0, 1);
                break;
            case 4096:
                sr.color = new Color(0, 0, 0.6f);
                break;
            default:
                sr.color = new Color(1, 0.2f, 0.2f);
                break;
        }
    }

    private void Update()
    {
        if(tf.localPosition != Vector3.zero)
        {
            tf.localPosition = new Vector3(tf.localPosition.x / 1.05f, tf.localPosition.y / 1.05f);
            if (tf.localPosition.x < 0.1f && tf.localPosition.x > -0.1f && tf.localPosition.y < 0.1f && tf.localPosition.y > -0.1f)
            {
                tf.localPosition = Vector3.zero;
            }
        }
        merge();
    }

    public void updatePosition(int pos)
    {
        position = pos;
        tf.parent = board.GetComponent<Board>().getGrid()[pos].GetComponent<Transform>();
    }

    public void setMerge(GameObject o1, GameObject o2)
    {
        obj1 = o1;
        obj2 = o2;
    }

    private void merge()
    {
        if(obj1 == null)
        {
            return;
        }
        sr.enabled = false;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        if (obj1.transform.localPosition == Vector3.zero && obj2.transform.localPosition == Vector3.zero)
        {
            Destroy(obj1);
            Destroy(obj2);
            sr.enabled = true;
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            getColor(value);
            obj1 = null;
            obj2 = null;
        }
    }

    private void addText()
    {
        GameObject text = new GameObject("value");
        TextMesh mesh = text.AddComponent<TextMesh>();
        Transform tf = text.GetComponent<Transform>();
        SortingGroup sortGroup = text.AddComponent<SortingGroup>();
        sortGroup.sortingOrder = 5;
        tf.parent = gameObject.transform;
        tf.localPosition = Vector3.zero;
        mesh.text = value.ToString();
        mesh.alignment = TextAlignment.Center;
        mesh.anchor = TextAnchor.MiddleCenter;
        mesh.fontSize = 100;
        mesh.characterSize = 0.05f;
        mesh.fontStyle = FontStyle.Normal;
        mesh.color = Color.black;
    }
}
