using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Mapmanager : MonoBehaviour
{
    public static Mapmanager instance;
    public Tilemap tilemap;
    public List<Tile> tiles = new List<Tile>();

    public int width, height;
    public Maptile[ , ] Map ;
    public int a;
    public int b;
    
    public enum tiletypes 
    {
        grass = 0,
        water = 1,
    };
    void Start()
    {
        
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        //map generation
        Map = new Maptile[width, height];
        float xoff = Random.Range(1, 1000000);
        float yoff = Random.Range(1, 100000);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float sample = Mathf.PerlinNoise((float)i / width + xoff, (float)j / height + yoff);
                
                if (sample <= 0.3)
                {
                    //water
                    Map[i, j] = new Maptile(tiletypes.water);
                } else
                {
                    //grass
                    Map[i, j] = new Maptile(tiletypes.grass);
                }
            }
        }

        
        UpdateTilemap();
    }
    /// <summary>
    /// Updates the Tilemap's sprites
    /// </summary>
    public void UpdateTilemap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), tiles[(int)Map[i, j].type]); 
            }
        }
    }

    public bool placing(Building building_type, Vector2Int building_position)
    {
        
        foreach(Vector2Int b_p in building_type.The_offset_of_tiles)
        {
            Maptile G_T_P = Get_tile_pos(building_position + b_p);
            if (G_T_P.type == tiletypes.water || G_T_P.building)
            {
                return false;
            }
        }
        foreach (Vector2Int b_p in building_type.The_offset_of_tiles)
        {
            Maptile G_T_P = Get_tile_pos(building_position + b_p);
            G_T_P.building = building_type;
        }
        building_type.transform.position = (Vector2)building_position;
        return true;
    }
    public void Is_button_clicked(Building building)
    {
        
         if(placing(building, new Vector2Int(a, b)))
        {
            Instantiate(building);
        }
        
        
    }
    public Maptile Get_tile_pos(Vector2Int offset)
    {
        return Map [offset.x, offset.y];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
