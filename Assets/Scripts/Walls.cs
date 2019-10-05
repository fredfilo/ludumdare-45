using UnityEngine;
using UnityEngine.Tilemaps;

public class Walls : MonoBehaviour
{
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        GetComponent<Tilemap>().color = new Color(0,0,0,0);
    }
}
