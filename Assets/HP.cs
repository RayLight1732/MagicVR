using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int maxHP;

    private int _hp;
    private int hp
    {
        set {
            _hp = value;
            if (OnChangeHandler != null)
            {
                OnChangeHandler(this, value);
            }
        }
        get => _hp;
    }

    public event EventHandler<int> OnChangeHandler;

    private void OnEnable()
    {
        _hp = maxHP;
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHP(int hp)
    {
        if (hp <= maxHP)
        {
            this.hp = hp;
        }
    }

    public int getHP() { return hp; }


    public void addHP(int value)
    {
        hp = Math.Min(hp + value, maxHP);
    }
    public void removeHP(int value)
    {
        hp = Math.Max(hp - value, 0);
    }
}
