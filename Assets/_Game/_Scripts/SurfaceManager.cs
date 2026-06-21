using System.Collections.Generic;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radius;

    public ClickableSurface ResolveSurface(Vector2 worldPos, bool preferLower)
    {
        // 1. Get ALL colliders under this X position
        Collider2D[] cols = Physics2D.OverlapCircleAll(worldPos, radius, groundLayer);

        if (cols.Length == 0)
            return null;

        ClickableSurface best = null;
        
        foreach (var col in cols)
        {
            var surface = col.GetComponent<ClickableSurface>();
            if (!surface) continue;

            if (best == null)
            {
                best = surface;
                continue;
            }
            if (preferLower)
            {
                if (surface.heightLevel < best.heightLevel)
                    best = surface;
            }
            else
            {
                if (surface.heightLevel > best.heightLevel)
                    best = surface;
            }
        }

        return best;
    }
}