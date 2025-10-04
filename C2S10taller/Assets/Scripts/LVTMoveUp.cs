using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class LVTMoveUp : MonoBehaviour
{
    public int frames = 30;
    public float risePerFrame = 0.02f;

    Rigidbody2D rb;
    bool moving = false;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    // método público para que el detector lo invoque
    public void StartMove()
    {
        if (!moving) StartCoroutine(MoveUpRoutine());
    }

    IEnumerator MoveUpRoutine()
    {
        moving = true;
        for (int i = 0; i < frames; i++)
        {
            rb.MovePosition(rb.position + Vector2.up * risePerFrame);
            yield return new WaitForFixedUpdate();
        }
        moving = false;
    }
}


