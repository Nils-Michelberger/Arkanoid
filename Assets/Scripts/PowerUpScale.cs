using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScale : Powerup
{
    private void OnTriggerEnter(Collider other)
    {
        ToggleVisibilityAndCollider(false);

        Paddle paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
        var localScalePaddle = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(localScalePaddle.x * 2f, localScalePaddle.y, localScalePaddle.z);

        StartCoroutine(PowerUpActive(paddle));
    }

    private IEnumerator PowerUpActive(Paddle paddle)
    {
        yield return new WaitForSeconds(15f);

        var localScalePaddle = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(localScalePaddle.x / 2f, localScalePaddle.y, localScalePaddle.z);
        
        StartTimerForPowerUp();
    }
}
