using UnityEngine;

public class ObjectGetter : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private GameObject filter;
    [SerializeField]
    private GameObject lefthand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFilter() { return filter; }

    public GameObject GetLeftHand() { return lefthand; }
}
