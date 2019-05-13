using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    //make a variable called target
    public Transform Player;
    //the speed of camera
    public float Speed = 0.125f;
    //3x1 matrix for offsetting between player and camera
    public Vector3 offset;

    void FixedUpdate()
    {
        //add player the ON-GOING position then add the offset you will get
        //updated position on where the camera should follow
        transform.position = Player.position + offset;  
    }
}
