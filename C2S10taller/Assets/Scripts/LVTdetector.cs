using UnityEngine;

public class LVTDetector : MonoBehaviour
{
    public LVTMoveUp platform; // arrastrar el TilemapLVT aquí en el inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platform.StartMove();
            // parent para que el player suba pegado al platform
            other.transform.SetParent(platform.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // desparentar solo si estaba parent al platform
            if (other.transform.parent == platform.transform)
                other.transform.SetParent(null);
        }
    }
}
