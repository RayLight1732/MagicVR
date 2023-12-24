using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float EnemySpeed;
    public float AttackDistance; // プレイヤーとの攻撃距離の閾値
    public float ChangeTime;

    float Timer;
    public Animator EnemyController;
    private GameObject Target;
    private bool isInAttackMode; // 攻撃モードかどうかを示すフラグ

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        var speed = Vector3.zero;
        speed.z = EnemySpeed;
        var rot = transform.eulerAngles; 
        if (Target)
        {
            transform.LookAt(Target.transform);
            rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;

            // プレイヤーとの距離を計算
            float distanceToPlayer = Vector3.Distance(transform.position, Target.transform.position);

            // プレイヤーが攻撃範囲内にいるか判断
            if (distanceToPlayer <= AttackDistance)
            {
                EnemyController.SetBool("Attack",true);
                // ここに攻撃のロジックを追加

            }
            else
            {
                // 追跡モードのロジック
                MoveTowardsTarget();
                EnemyController.SetBool("Attack", false);
                //this.transform.Translate(speed);
            }
        }
        else
        {
            RandomMovement();
        }
    }

    private void MoveTowardsTarget()
    {
        var speed = new Vector3(0, 0, EnemySpeed);
        transform.Translate(speed * Time.deltaTime);
    }

    private void RandomMovement()
    {
        // ランダムな方向転換のロジック
        var rot = transform.eulerAngles;
        Timer += Time.deltaTime;
            if(ChangeTime <= Timer)
            {
                float rand = Random.Range(0, 360);
                rot.y = rand;
                Timer = 0;
            }
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;
        var speed = new Vector3(0, 0, EnemySpeed);
        transform.Translate(speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Target = other.gameObject;
            EnemyController.SetBool("Run", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Target = null;
            EnemyController.SetBool("Run", false);
        }
    }
}
