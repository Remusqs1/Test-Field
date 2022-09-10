using UnityEngine;

public enum SpawnOption
{
    Create,
    Delete
}

public class SpawnItemController : MonoBehaviour
{
    [SerializeField] GameObject gameObjectItem;
    [SerializeField] SpawnOption spawnOption;
    Transform spawnTransform;
    public int spawnOffsetX = 0;
    public int spawnOffsetY = 0;

    GameObject mySpawnObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnTransform = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (spawnOption == SpawnOption.Create)
            {
                GameObject instantiatedObject = Instantiate(gameObjectItem, new Vector3(spawnTransform.position.x + spawnOffsetX, spawnTransform.position.y + spawnOffsetY, spawnTransform.position.z), Quaternion.identity);
                instantiatedObject.tag = gameObjectItem.name+"_Clone";
            }
            else if (spawnOption == SpawnOption.Delete)
            {
                var objects = GameObject.FindGameObjectsWithTag(gameObjectItem.tag == "Untagged" ? gameObjectItem.name+"_Clone": gameObjectItem.tag);
                Destroy(objects.Length >= 1 ? objects[objects.Length - 1] : null);
            };
        }
    }
}