using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;
    public float speedX;
    public float speedZ;
    public GameObject gameStatsObject;
    private Ball oldBall;
    private AudioSource ballSound;
    private AudioSource powerUpSound;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        ballSound = audioSources[0];
        powerUpSound = audioSources[1];
        
        if (oldBall != null)
        {
            StartCoroutine(StartBallWithVelocity(oldBall.Velocity));
        }
        else
        {
            velocity = new Vector3(0, 0, -speedZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameStats gameStats = gameStatsObject.GetComponent<GameStats>();
        
        if (other.CompareTag("Paddle"))
        {
            float maxDist = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * speedX, velocity.y, -velocity.z);
            ballSound.Play();
        }
        else if (other.CompareTag("Wall"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            ballSound.Play();
        }
        else if (other.CompareTag("Block"))
        {
            gameStats.ScoreValue += 10;
            gameStats.score.text = gameStats.ScoreValue.ToString();
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            ballSound.Play();
        }
        else if (other.CompareTag("Wall Top"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            ballSound.Play();
        }
        else if (other.CompareTag("Border"))
        {
            if (gameStats.BallsCount <= 1)
            {
                gameStats.HealthValue--;
                if (gameStats.HealthValue <= 0) 
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                gameStats.health.text = gameStats.HealthValue.ToString();
                
                StartCoroutine(ResetBall());
                ballSound.Play();
            }
            else
            {
                ballSound.Play();
                gameStats.BallsCount--;
                StartCoroutine(DestroyAfterOneSecond());
            }
        }
        else if (other.CompareTag("PowerUpMulti") || other.CompareTag("PowerUpScale"))
        {
            powerUpSound.Play();
        }
    }

    private IEnumerator ResetBall()
    {
        velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0.5f, -2f);
        yield return new WaitForSeconds(2f);
        velocity = new Vector3(0, 0, -speedZ);
    }

    private IEnumerator StartBallWithVelocity(Vector3 newVelocity)
    {
        velocity = new Vector3(0, 0, 0);
        var angle = Math.Atan2(newVelocity.x, newVelocity.z) * Mathf.Rad2Deg + 180;
        GameObject newArrow = Instantiate(arrow, gameObject.transform.position, Quaternion.Euler(new Vector3(0, Convert.ToSingle(angle), 0)));
        newArrow.SetActive(true);
        StartCoroutine(BlinkArrow(newArrow));
        yield return new WaitForSeconds(2f);
        velocity = newVelocity;
        yield return new WaitForSeconds(1f);
        Destroy(newArrow);
    }

    private IEnumerator DestroyAfterOneSecond()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private IEnumerator BlinkArrow(GameObject arrow)
    {
        Renderer[] rs = arrow.GetComponentsInChildren<Renderer>();
        
        showArrow(rs, true);
        yield return new WaitForSeconds(0.4f);
        showArrow(rs, false);
        yield return new WaitForSeconds(0.4f);
        showArrow(rs, true);
        yield return new WaitForSeconds(0.4f);
        showArrow(rs, false);
        yield return new WaitForSeconds(0.4f);
        showArrow(rs, true);
        yield return new WaitForSeconds(0.4f);
        showArrow(rs, false);
    }

    private static void showArrow(Renderer[] rs, bool visible)
    {
        foreach (Renderer r in rs)
        {
            r.enabled = visible;
        }
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public Ball OldBall
    {
        get => oldBall;
        set => oldBall = value;
    }
}
