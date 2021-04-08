using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    // TODO background sprite has border (not seamless)
    
    [SerializeField] private float speed;
    public GameObject[] sprites;
    private BackgroundLoop backgroundLoop;
    //private Camera mainCamera;
    private Vector2 screenBounds;
    
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundLoop = gameObject.GetComponent<BackgroundLoop>();
        //mainCamera = gameObject.GetComponent<Camera>();
        //screenBounds =
        //    mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in sprites)
        {
            loadChildObjects(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            // skip over the parent
            if (!(child == children[0]))
            {
                child.Translate(0, -1f * speed * Time.deltaTime, 0, Space.World);

                // check if image out of bounds
                float objectHeight = child.GetComponent<MeshRenderer>().bounds.size.y;
                if (child.transform.position.y < -objectHeight)
                {
                    // if child out of bounds create new clone and place it on top
                    // and destroy child
                    GameObject clone = Instantiate(child.gameObject) as GameObject;
                    clone.transform.Translate(new Vector3(0, 2f * objectHeight, 0));
                    clone.transform.SetParent(transform);
                    Destroy(child.gameObject);
                }
            }
        }
    }

    void loadChildObjects(GameObject obj)
    {
        // clone each sprite and put it on top, so that there are two sprites
        float objectHeight = obj.GetComponent<MeshRenderer>().bounds.size.y;
        GameObject clone = Instantiate(obj) as GameObject;
        clone.transform.Translate(new Vector3(0, objectHeight, 0));
        float cloneScaleX = clone.transform.localScale.x;
        /*
        clone.transform.localScale += new Vector3(-2 * cloneScaleX, 1f, 1f);
        clone.transform.Rotate(0f, 0f, 180f, Space.Self);
        */
        clone.transform.SetParent(transform);
    }
    
}
