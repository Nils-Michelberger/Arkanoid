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
    public TextMeshProUGUI health;
    public TextMeshProUGUI score;
    private int scoreValue = 0;
    private int healthValue = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0, -speedZ);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            float maxDist = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * speedX, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("Wall"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        }
        else if (other.CompareTag("Block"))
        {
            scoreValue += 10;
            score.text = scoreValue.ToString();
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("Border"))
        {
            healthValue--;
            if (healthValue <= 0) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            health.text = healthValue.ToString();
            StartCoroutine(ResetBall());
        }
        GetComponent<AudioSource>().Play();
    }

    private IEnumerator ResetBall()
    {
        velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0.5f, -2f);
        yield return new WaitForSeconds(2f);
        velocity = new Vector3(0, 0, -speedZ);
    }
}
