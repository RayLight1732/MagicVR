using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/*
 *É`ÉÉÅ[ÉWÇÃèÛë‘Çä«óùÇ∑ÇÈ 
 */
public class ChargeStateManager : MonoBehaviour
{
    public enum ChargeState
    {
        Deploy,
        Charging,
        Destroy
    }

    [SerializeField]
    private AudioSource aura;
    [SerializeField]
    private AudioSource failSound;


    private ChargeState state = ChargeState.Deploy;

    private void Awake()
    {
        state = ChargeState.Deploy;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool start = false;
    private float fadeIn = 0;
    private bool end = false;
    private float fadeOut = 0;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (fadeIn < 1)
            {
                if (fadeIn == 0)
                {
                    aura.Play();
                }
                fadeIn += Time.deltaTime;
                aura.volume = fadeIn;
            }
            else
            {
                start = false;
            }
        } else if (end)
        {
            if (fadeOut > 0)
            {
                fadeOut -= Time.deltaTime;
                aura.volume = fadeOut;
            }
            else
            {
                aura.Stop();
                end = false;
            }
        }
    }

    public void EndDeployment()
    {
        state = ChargeState.Charging;
    }

    public ChargeState GetState()
    {
        return state;
    }

    private bool markDestroy = false;

    public void SetStateDestroy(bool fail)
    {
        state = ChargeState.Destroy;
        if (!markDestroy)
        {
            markDestroy = true;
            GetComponent<Animator>().SetTrigger("shrink");
            if (fail)
            {
                Debug.Log("PlaySound");
                failSound.Play();
            }
        }
    }

    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }

    public void OnChargeStart()
    {
        start = true;
        fadeIn = 0;
    }

    public void OnChargeEnd()
    {
        end = true;
        fadeOut = 1;
    }
}
