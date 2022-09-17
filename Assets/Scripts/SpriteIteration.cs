using System.Collections;
using UnityEngine;

public class SpriteIteration : MonoBehaviour
{
    [SerializeField] Sprite[] spriteRenderers;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeSprite", 0, 0.2f);
    }

    void ChangeSprite()
    {
        var currentSprite = sr.sprite;
        Sprite[] spriteList = spriteRenderers;
        int spriteIndex = 0;

        if (currentSprite != null)
        {
            spriteIndex = UnityEngine.Random.Range(0, spriteList.Length - 1);
        }
        spriteIndex = (spriteIndex > spriteList.Length-1 || spriteIndex < 0) ? 0 : spriteIndex;
        sr.sprite = spriteList[spriteIndex];
    }
}
