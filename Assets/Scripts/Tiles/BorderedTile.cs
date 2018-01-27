using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BorderedTile : Tile
{
    public Sprite[] sprites;

    public Sprite preview;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = 1; y >= -1; y--)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int neighborPosition = position + new Vector3Int(x, y, 0);
                if (HasSameTileType(tilemap, neighborPosition))
                {
                    tilemap.RefreshTile(neighborPosition);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        string neighbors = string.Empty;

        for (int y = 1; y >= -1; y--) // Iterate from top-left to bottom-right
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int neighborPosition = position + new Vector3Int(x, y, 0);
                if (HasSameTileType(tilemap, neighborPosition))
                {
                    neighbors += '1';
                }
                else
                {
                    neighbors += '0';
                }
            }
        }

        string adjacentNeighbors = new string(new char[] { neighbors[1], neighbors[3], neighbors[5], neighbors[7] });
        string diagonalNeighbors = new string(new char[] { neighbors[0], neighbors[2], neighbors[6], neighbors[8] });

        switch (adjacentNeighbors)
        {
            case "0011":
                tileData.sprite = sprites[0];
                break;
            case "0111":
            case "0001":
                tileData.sprite = sprites[1];
                break;
            case "0101":
                tileData.sprite = sprites[2];
                break;
            case "1011":
            case "0010":
                tileData.sprite = sprites[3];
                break;
            case "1101":
            case "0100":
                tileData.sprite = sprites[5];
                break;
            case "1010":
                tileData.sprite = sprites[6];
                break;
            case "1110":
            case "1000":
                tileData.sprite = sprites[7];
                break;
            case "1100":
                tileData.sprite = sprites[8];
                break;
            default:
                if (sprites.Length < 13)
                {
                    tileData.sprite = sprites[4];
                    break;
                }
                switch (diagonalNeighbors)
                {
                    case "1110":
                        tileData.sprite = sprites[9];
                        break;
                    case "1101":
                        tileData.sprite = sprites[10];
                        break;
                    case "1011":
                        tileData.sprite = sprites[11];
                        break;
                    case "0111":
                        tileData.sprite = sprites[12];
                        break;
                    default:
                        tileData.sprite = sprites[4];
                        break;
                }
                break;
        }
    }

    private bool HasSameTileType(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/Bordered Tile")]
    public static void CreateTileAsset()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Bordered Tile", "Bordered Tile", "asset", "Save Tile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BorderedTile>(), path);
    }
#endif
}
