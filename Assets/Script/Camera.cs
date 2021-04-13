using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        gameObject.transform.position = Player.transform.position - new Vector3(0, Player.transform.position.y, 15);
    }
}
