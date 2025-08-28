using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color color
    {
        set
        {
            spriteRenderer.color = value;
        }

        get
        {
            return spriteRenderer.color;
        }
    }

    public int sortingOrder
    {
        set
        {
            spriteRenderer.sortingOrder = value;
        }

        get
        {
            return spriteRenderer.sortingOrder;
        }
    }

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("You need to SpriteRenderer for Block");
        }
    }

    public static GameObject CreateTile(Transform parent, Vector2 position, Color color, int order = 1, bool pooling = false)
    {
        GameObject go = Managers.Resource.Instantiate("Tile", parent, pooling: pooling);
        go.transform.parent = parent;
        go.transform.localPosition = position;

        Tile tile = Utils.GetOrAddComponent<Tile>(go);
        tile.color = color;
        tile.sortingOrder = order;

        return go;
    }
}
