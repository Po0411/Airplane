using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State : int
    {
        None = -1,  // 사용전
        Ready = 0,  // 준비 완료
        Appear,     // 등장
        Battle,     // 전투중
        Dead,       // 사망
        Disappear,  // 퇴장
    }

    [SerializeField]
    State CurrentState = State.None;

    /// 최고 속도
    const float MaxSpeed = 10.0f;

    const float MaxSpeedTime = 0.5f;

    [SerializeField]
    Vector3 TargetPosition;

    [SerializeField]
    float CurrentSpeed;

    Vector3 CurrentVelocity;

    float MoveStartTime = 0.0f;

    [SerializeField]
    Transform FireTransform;

    [SerializeField]
    GameObject Blluet;

    [SerializeField]
    float BulletSpeed = 1;

    float LastActionUpdateTime = 0.0f;

    [SerializeField]
    int FireRemainCount = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            Appear(new Vector3(7.0f, 0.0f, 0.0f));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Disappear(new Vector3(-15.0f, 0.0f, 0.0f));
        }
        */
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            Appear(new Vector3(7.0f, transform.position.y, transform.position.z));
        }
        */
        switch (CurrentState)
        {
            case State.None:
            case State.Ready:
                break;
            case State.Dead:
                break;
            case State.Appear:
            case State.Disappear:
                UpdateSpeed();
                UpdateMove();
                break;
            case State.Battle:
                UpdateBattle();
                break;

        }

    }

    void UpdateSpeed()
    {
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, (Time.time - MoveStartTime) / MaxSpeedTime);
    }

    void UpdateMove()
    {
        float distance = Vector3.Distance(TargetPosition, transform.position);
        if (distance == 0)
        {
            Arrived();
            return;
        }

        CurrentVelocity = (TargetPosition - transform.position).normalized * CurrentSpeed;

        // 속도 = 거리 / 시간 이므로 시간 = 거리 / 속도
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref CurrentVelocity, distance / CurrentSpeed, MaxSpeed);
    }

    void Arrived()
    {
        CurrentSpeed = 0.0f;

        if (CurrentState == State.Appear)
        {
            CurrentState = State.Battle;
            LastActionUpdateTime = Time.time;
        }
        else // if (CurrentState == State.Disappear)
        {
            CurrentState = State.None;
        }

    }

    public void Appear(Vector3 targetPos)
    {
        TargetPosition = targetPos;
        CurrentSpeed = MaxSpeed;

        CurrentState = State.Appear;
        MoveStartTime = Time.time;
    }

    void Disappear(Vector3 targetPos)
    {
        TargetPosition = targetPos;
        CurrentSpeed = 0.0f;

        CurrentState = State.Disappear;
        MoveStartTime = Time.time;
    }

    void UpdateBattle()
    {
        if (Time.time - LastActionUpdateTime > 1.0f)
        {
            if(FireRemainCount > 0)
            {
                Fire();
                FireRemainCount--;
            }
            else
            {
                Disappear(new Vector3(-15.0f, transform.position.y, transform.position.z));
            }

            LastActionUpdateTime = Time.time;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("other" + other.name);

        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            player.OnCrash(this);
        }

    }

    public void OnCrash(Player player)
    {
        Debug.Log("OnCrash" + player.name);
    }

    public void Fire()
    {
        GameObject go = Instantiate(Blluet);

        Bullet blluet = go.GetComponent<Bullet>();
        blluet.Fire(OwnerSide.Enemy, FireTransform.position, -FireTransform.right, BulletSpeed);

    }
}
