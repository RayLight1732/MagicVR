using UnityEngine;

public class PlayerHPCallback : MonoBehaviour
{
    private const string propertyName = "_RedStrength";

    [SerializeField]
    public GameObject filter;
    private GameObject gameoverUI;
    private VRPlayerController vRPlayerController;
    private ObjectGetter objectGetter;

    private void Awake()
    {
        HP hp = gameObject.GetComponent<HP>();
        hp.OnChangeHandler += OnHPChange;
    }

    void OnHPChange(object sender, double hpValue)
    {
        HP hpComponent = (HP)sender;
        float hpPercent = (float)(hpValue / hpComponent.maxHP);
        var material = filter.GetComponent<Renderer>().material;
        if (material.HasProperty(propertyName))
        {
            material.SetFloat(propertyName, hpPercent);
            if(hpValue<=0){
                Gameover();
            }
        }
        else
        {
            Debug.Log(propertyName + " was not found in filter renderer");
        }
    }

    private void Gameover(){
        vRPlayerController =  this.GetComponent<VRPlayerController>();
        
        Time.timeScale = 0;
    }
}