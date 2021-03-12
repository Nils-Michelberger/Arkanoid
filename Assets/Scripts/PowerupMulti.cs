using System;
using UnityEngine;

public class PowerupMulti : Powerup
{
    private void OnTriggerEnter(Collider other)
    {
        ToggleVisibilityAndCollider(false);

        var position = other.transform.position;
        GameObject newBall = Instantiate(other.gameObject, new Vector3(position.x + 2f, position.y, position.z),
            Quaternion.identity);
        
        GameObject.Find("GameStats").GetComponent<GameStats>().BallsCount++;
        
        newBall.GetComponent<Ball>().OldBall = other.GetComponent<Ball>();
        StartTimerForPowerUp();
    }
}