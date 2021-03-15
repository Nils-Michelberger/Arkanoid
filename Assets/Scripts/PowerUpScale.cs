using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScale : Powerup
{
    public Material redMaterial;
    
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
        yield return new WaitForSeconds(13f);

        StartCoroutine(BlinkPaddle());
        yield return new WaitForSeconds(2f);

        var localScalePaddle = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(localScalePaddle.x / 2f, localScalePaddle.y, localScalePaddle.z);
        
        StartTimerForPowerUp();
    }
    
    private IEnumerator BlinkPaddle()
    {
        Paddle paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
        Material blueMaterial = paddle.GetComponent<Renderer>().sharedMaterial;

        paddle.GetComponent<Renderer>().sharedMaterial = redMaterial;
        yield return new WaitForSeconds(0.4f);
        paddle.GetComponent<Renderer>().sharedMaterial = blueMaterial;
        yield return new WaitForSeconds(0.4f);
        paddle.GetComponent<Renderer>().sharedMaterial = redMaterial;
        yield return new WaitForSeconds(0.4f);
        paddle.GetComponent<Renderer>().sharedMaterial = blueMaterial;
        yield return new WaitForSeconds(0.4f);
        paddle.GetComponent<Renderer>().sharedMaterial = redMaterial;
        yield return new WaitForSeconds(0.4f);
        paddle.GetComponent<Renderer>().sharedMaterial = blueMaterial;
    }
}
