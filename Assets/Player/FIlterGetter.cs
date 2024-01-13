using UnityEngine;

public class FilterGetter : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private GameObject filter;
    [SerializeField]
    private GameObject leftController;
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject cameraOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFilter() { return filter; }

    public GameObject GetLeftController() { return leftController; }

    public GameObject GetHead()
    {
        return head;
    }

    public GameObject GetCameraOffset() {  return cameraOffset; }
}
