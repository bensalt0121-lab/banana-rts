using UnityEngine;

public class Maptile
{
    public Mapmanager.tiletypes type;
    public Building building;

    // Constructor to set tile
    public Maptile(Mapmanager.tiletypes _type)
    {
        type = _type;
    }
}
