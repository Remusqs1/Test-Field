using UnityEngine;
using System.Linq;

public enum SpawnOption
{
    Create,
    Delete
}

public class SpawnItemController : MonoBehaviour
{
    [SerializeField] GameObject gameObjectItem;
    [SerializeField] SpawnOption spawnOption;
    [SerializeField] GameObject spawner;
    Transform spawnTransform;
    string cloneTag;
    GameObject clone;

    public float spawnOffsetX = 0f;
    public float spawnOffsetY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cloneTag = gameObjectItem.tag == "Untagged" ? gameObjectItem.name + "_Clone" : gameObjectItem.tag + "_Clone";
        if (spawnOption == SpawnOption.Delete)
        {
            spawnTransform = spawner.transform;
            spawnOffsetX = spawner.GetComponent<SpawnItemController>().spawnOffsetX;
            spawnOffsetY = spawner.GetComponent<SpawnItemController>().spawnOffsetY;
        }
        else
        {
            spawnTransform = this.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var instantiatedObjects = GameObject.FindGameObjectsWithTag(cloneTag);
            Vector3 spawnPosition = new Vector3(spawnTransform.position.x + spawnOffsetX, spawnTransform.position.y + spawnOffsetY, spawnTransform.position.z);
            var targetGameObject = instantiatedObjects.Where(x => x.transform.position == spawnPosition).ToList();

            if (spawnOption == SpawnOption.Create && !targetGameObject.Any())
            {
                clone = Instantiate(gameObjectItem, spawnPosition, Quaternion.identity);
                clone.tag = cloneTag;
            }
            else if (spawnOption == SpawnOption.Delete && targetGameObject.Any())
            {
                Destroy(targetGameObject.First());
            }
        }
    }
} 