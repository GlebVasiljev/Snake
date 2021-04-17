using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float EnemyFOV;
    public Transform EnemyBody;
    public List<Transform> Paths;
    public float EnemySpeed = 3f;
    private int CurrentPosition;
    private Transform SnakePos;
    public Transform RayCastTransform;


    private void Start()
    {
        EnemyBody.position = Paths[0].position;
    }
    private void Update()
    {
        HaveSeePlayer();
        if (SnakePos)
        {
            MoveBody(SnakePos);
            if(Vector2.Distance(EnemyBody.position, SnakePos.position) > 15)
            {
                SnakePos = null;
            }
            return;
        }

        MoveToPath();


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(RayCastTransform.position, RayCastTransform.right * EnemyFOV);
        
    }

    bool HaveSeePlayer()
    {

        RaycastHit2D Hit = Physics2D.CircleCast( RayCastTransform.position, 3 , RayCastTransform.right, EnemyFOV);
        if(Hit.collider == null)
        {
           //  Debug.Log("No raycast");
            return false;
            
        }
        SnakeControll snake = Hit.collider.GetComponentInParent<SnakeControll>();

        if(snake == null)
        {
            // Debug.Log("No Snake  " + Hit.collider.name);

            return false;
        }

        SnakePos = snake.Head.transform;
         // Debug.Log("See player ");
        return true;
    }

    void MoveBody(Transform Target)
    {
        Vector3 NextPos = Vector3.MoveTowards(EnemyBody.position, Target.position, Time.deltaTime * EnemySpeed);
        EnemyBody.position = NextPos;
        EnemyBody.right = Target.position - EnemyBody.position;

    }
    void MoveToPath()
    {
        int Pos = CurrentPosition == Paths.Count - 1 ? 0 : CurrentPosition + 1; // if
        MoveBody(Paths[Pos]);
        

        if (Vector2.Distance(EnemyBody.position, Paths[Pos].position) < 0.01f)
        {
            CurrentPosition++;
            if (CurrentPosition == Paths.Count) // To start pos
                CurrentPosition = 0;
        }
    }
}
