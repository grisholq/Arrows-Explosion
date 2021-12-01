using DCFAEngine;
using UnityEngine;

public class GameRegion2D : MonoBehaviour, ISpawner
{
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector3 center;
    [Layer(true)]
    public int layerMask;

    private void OnDrawGizmos()
    {
        DrawRect(GetCenter());
    }

    public Vector3 GetCenter()
    {
        Vector3 result = transform.position;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, layerMask))
        {
            result = hitInfo.point;
        }
        return result + center;
    }

    private void DrawRect(Vector3 pos)
    {
        pos += Vector3.up * 0.1f;
        Gizmos.color = Color.red;
        Vector2 halfsize = size / 2f;
        Gizmos.DrawLine(new Vector3(-halfsize.x, 0, -halfsize.y) + pos, new Vector3( halfsize.x, 0, -halfsize.y) + pos);
        Gizmos.DrawLine(new Vector3(-halfsize.x, 0, -halfsize.y) + pos, new Vector3(-halfsize.x, 0,  halfsize.y) + pos);
        Gizmos.DrawLine(new Vector3( halfsize.x, 0, -halfsize.y) + pos, new Vector3( halfsize.x, 0,  halfsize.y) + pos);
        Gizmos.DrawLine(new Vector3(-halfsize.x, 0,  halfsize.y) + pos, new Vector3( halfsize.x, 0,  halfsize.y) + pos);
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 center = GetCenter();
        //Vector2 halfsize = size / 2f;
        //Vector2 pos2d = new Vector2(Random.Range(0, size.x), Random.Range(0, size.y)) - halfsize;
        Vector2 pos2d = (Quasi2DRandom.Global.Random2D() - Vector2.one * 0.5f) * size;
        return center + pos2d.XoY();
    }
}
