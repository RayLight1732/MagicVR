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
    private TimelineAsset deleteTimeline;
    [SerializeField]
    private TimelineAsset faileDeleteTimeline;

    private PlayableDirector director;

    private ChargeState state = ChargeState.Deploy;

    private void Awake()
    {
        state = ChargeState.Deploy;
        director = GetComponent<PlayableDirector>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (!fail)
            {
                director.Play(deleteTimeline);
            }
            else
            {
                director.Play(faileDeleteTimeline);
            }
        }
    }

    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
