using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum OwnerSide : int
{
    Player = 0,
    Enemy
}


public class Bullet : MonoBehaviour
{
    const float LifeTime = 15.0f;    // 총알의 생존 시간

    OwnerSide ownerSde = OwnerSide.Player;

    [SerializeField]
    Vector3 MoveDirection = Vector3.up;

    [SerializeField]
    float Speed = 0.0f;

    bool NeedMove = false;

    float FiredTime;

    bool Hited = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ProcessDisappearCondition())
            return;

        UpdateMove();
    }

    void UpdateMove()
    {
        if (!NeedMove)
        {
            return;
        }

        Vector3 moveVector = MoveDirection.normalized *  Speed * Time.deltaTime;
        moveVector = AdjustMove(moveVector);
        transform.position += moveVector; 
    }

    public void Fire(OwnerSide FireOwner, Vector3 firePosition, Vector3 direction, float speed)
    {
        ownerSde = FireOwner;
        transform.position = firePosition;
        MoveDirection = direction;
        Speed = speed;

        NeedMove = true;
        FiredTime = Time.time;
    }

    Vector3 AdjustMove(Vector3 moveVector)
    {
        // 레이캐스트 힛 초기화
        RaycastHit hitInfo;

        if (Physics.Linecast(transform.position, transform.position + moveVector, out hitInfo))
        {
            moveVector = hitInfo.point - transform.position;
            OnBulletCollision(hitInfo.collider);
        }
        return moveVector;
    }

    void OnBulletCollision(Collider collider)
    {
        if (Hited)
        {
            return;
        }

        Collider myCollider = GetComponentInChildren<Collider>();
        myCollider.enabled = false;

        Hited = true;
        NeedMove = false;



        //Debug.Log("OnBulletCollision collider = " + collider.name); //출력 값을 확인

        if (ownerSde == OwnerSide.Player)
        {
            Enemy enemy = collider.GetComponentInParent<Enemy>();
        }
        else
        {
            Player player = collider.GetComponentInParent<Player>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBulletCollision(other);
    }

    bool ProcessDisappearCondition()
    {
        if (transform.position.x > 15.0f || transform.position.x < -15.0f
            || transform.position.y > 15.0f || transform.position.y < -15.0f)
        {
            Disappear();
            return true;
        }
        else if (Time.time - FiredTime > LifeTime)
        {
            Disappear();
            return true;
        }

        return false;
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
