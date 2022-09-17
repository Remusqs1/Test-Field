using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public enum SpawnOption
{
    Create,
    Delete
}

public class SpawnItemController : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] GameObject gameObjectItem;
    [SerializeField] SpawnOption spawnOption;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject[] itemList;
    #endregion

    #region Private variables
    Transform spawnTransform;
    string cloneTag;
    GameObject clone;
    #endregion

    #region Public variables
    public float spawnOffsetX = 0f;
    public float spawnOffsetY = 0f;
    public bool isRandom = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (!isRandom)
        {
            cloneTag = gameObjectItem.tag == "Untagged" ? gameObjectItem.name + "_Clone" : gameObjectItem.tag + "_Clone";
        }
        else
        {
            cloneTag = "Item";
        }

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
            Vector3 spawnPosition = new Vector3(spawnTransform.position.x + spawnOffsetX, spawnTransform.position.y + spawnOffsetY, spawnTransform.position.z);
            
           List<GameObject> instantiatedObjects = GameObject.FindGameObjectsWithTag(cloneTag).ToList();
           GameObject targetGameObject = instantiatedObjects.Where(x => x.transform.position == spawnPosition).FirstOrDefault();

            if (!isRandom)
            {
                if (spawnOption == SpawnOption.Create && targetGameObject == null)
                {
                    clone = Instantiate(gameObjectItem, spawnPosition, Quaternion.identity);
                    clone.tag = cloneTag;
                }
                else if (spawnOption == SpawnOption.Delete && targetGameObject != null)
                {
                    Destroy(targetGameObject);
                }
            }
            else
            {
                bool existGO = (targetGameObject != null) ? true : false;
                
                instantiatedObjects.ForEach(go => go.transform.position = spawnPosition);
                instantiatedObjects = itemList.ToList();

                GameObject go = existGO ? targetGameObject : null;
                if(existGO) Destroy(targetGameObject);
                clone = Instantiate(GetRandomGameObject(go), spawnPosition, Quaternion.identity);
                clone.tag = cloneTag;
            }
        }
    }

    private GameObject GetRandomGameObject(GameObject? currentGO)
    {
        var objectList = itemList;
        
        if (currentGO != null) objectList = objectList.Where(go => go.tag != GetTag(currentGO)).ToArray();

        GameObject newGO = objectList[UnityEngine.Random.Range(0, objectList.Length)];
        cloneTag = newGO.tag == "Untagged" ? newGO.name + "_Clone" : newGO.tag + "_Clone";
        return newGO;
    }

    private string GetTag(GameObject go)
    {
        return go.tag.Contains("_") ? go.tag.Substring(0, go.tag.IndexOf("_")) : go.tag;
    }
} 