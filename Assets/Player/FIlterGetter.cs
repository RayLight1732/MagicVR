using UnityEngine;

public class FilterGetter : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private GameObject filter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFilter() { return filter; }
}
