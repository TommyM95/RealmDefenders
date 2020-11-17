using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
