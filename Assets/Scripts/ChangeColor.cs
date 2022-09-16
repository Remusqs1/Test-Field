using System;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] SpriteRenderer sr;
    #endregion

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChangeObjectColor();
        }
    }

    private void ChangeObjectColor()
    {
        int randomInt =  UnityEngine.Random.Range(0,6);
        Color currentColor = sr.color;
        switch (randomInt)
        {
            case 0:
                sr.color = currentColor == Color.blue ? Color.magenta : Color.blue;
                break;
            case 1:
                sr.color = currentColor == Color.red ? Color.black : Color.red;
                break;
            case 2:
                sr.color = currentColor == Color.green ? Color.grey : Color.green;
                break;
            case 3:
                sr.color = currentColor == Color.yellow ? new Color(138,131,18) : Color.yellow; 
                break;
            case 4:
                sr.color = currentColor == Color.cyan ? new Color(166, 166, 0) : Color.cyan;
                break;
            case 5:
                sr.color = currentColor == Color.white ? new Color(21, 186, 99) : Color.white;
                break;
            default:
                break;
        }
    }
}
