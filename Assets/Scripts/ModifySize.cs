using UnityEngine;

public class ModifySize : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] GameObject targetGameObject;
    #endregion

    #region Private variables
    bool isActive;
    Transform originalTransform;
    Vector3 maxScale;
    float yOffsetFix = 0.5f;
    #endregion

    #region Public variables
    public bool isIncrease = true;
    public float sizeModifyFactor = 2;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        originalTransform = targetGameObject.transform;
        isActive = false;
        maxScale = new Vector3(originalTransform.localScale.x * sizeModifyFactor,
                               originalTransform.localScale.y * sizeModifyFactor,
                               originalTransform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) Resize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") isActive = true;
    }

    void Resize()
    {
        if (isIncrease)
        {
            if (targetGameObject.transform.localScale.magnitude > maxScale.magnitude) isActive = false;
            else
            {
                targetGameObject.transform.localScale = new Vector3(targetGameObject.transform.localScale.x * sizeModifyFactor, targetGameObject.transform.localScale.y * sizeModifyFactor, 1);
                FixYPosition();
            }
        }
        else
        {
            if (targetGameObject.transform.localScale.magnitude < maxScale.magnitude) isActive = false;
            else
            {
                targetGameObject.transform.localScale = new Vector3(targetGameObject.transform.localScale.x / sizeModifyFactor, targetGameObject.transform.localScale.y / sizeModifyFactor, 1);
                FixYPosition();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") isActive = false;
    }

    void FixYPosition()
    {
        targetGameObject.transform.position = new Vector3(targetGameObject.transform.position.x, originalTransform.position.y + yOffsetFix, 1);
    }

}