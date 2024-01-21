using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(MP))]
public class ChargeManager : MonoBehaviour
{

    [SerializeField]
    private GameObject magicCircle;
    [SerializeField]
    private double chargeDelay = 1;

    private bool charging = false;
    private MP mp;
    private double chargeDelay_ = 0;

    public double mpPerSecond = 1;

    private GameObject instaitiated;

    private void Awake()
    {
        mp = GetComponent<MP>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instaitiated != null)
        {
            ChargeStateManager chargeStateManager = instaitiated.GetComponent<ChargeStateManager>();
            if (chargeStateManager != null && chargeStateManager.GetState()==ChargeStateManager.ChargeState.Charging)
            {
                mp.AddMP(mpPerSecond*Time.deltaTime);
            }
        }
        if (chargeDelay_ > 0)
        {
            chargeDelay_ -= Time.deltaTime;
        }
    }

    public bool IsCharging()
    {
        return charging || chargeDelay_ > 0;
    }

    public void StartCharge()
    {
        if (!IsCharging())
        {
            instaitiated = Instantiate(magicCircle,transform.position,transform.rotation);
            Debug.Log("instantiate"+gameObject.transform.position);
            charging = true;
        }
    }

    public void StopCharge()
    {
        if (instaitiated != null)
        {
            instaitiated.GetComponent<ChargeStateManager>().SetStateDestroy(false);
            instaitiated = null;
        }
        charging = false;
        chargeDelay_ = chargeDelay;
    }

    public void FailToCharge()
    {
        if (instaitiated != null)
        {
            instaitiated.GetComponent<ChargeStateManager>().SetStateDestroy(false);
            instaitiated = null;
        }
        charging= false;
        chargeDelay_= chargeDelay;
    }
}
