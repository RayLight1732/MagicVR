using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int maxHP;
    public Image HPGage;

    private double _hp;
    private double hp
    {
        set {
            _hp = value;
            if (OnChangeHandler != null)
            {
                OnChangeHandler(this, value);
            }
            if(HPGage != null)
            {
                float percent = (float)_hp / maxHP;
                HPGage.fillAmount = percent;
            }
        }
        get => _hp;
    }

    public HP()
    {
        _hp = maxHP;
    }

    public event EventHandler<double> OnChangeHandler;

    private void OnEnable()
    {
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

    public void setHP(double hp)
    {
        if (hp <= maxHP)
        {
            this.hp = hp;
        }
    }

    public double getHP() { return hp; }


    public void addHP(double value)
    {
        hp = Math.Min(hp + value, maxHP);
    }
    public void removeHP(double value)
    {
        hp = Math.Max(hp - value, 0);
    }
    public void SetMaxHP(double maxHP){
        this.maxHP = (int)maxHP;
        if(hp > maxHP) {hp = maxHP;}
    }
}
