using UnityEngine;

//WORK IN PROGRESS... fk u collision
public class CollisionDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Enemy")
        {
            Debug.Log("Enemy hit!");
        }
    }
}
