using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public Transform playArea;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        float dir = Input.GetAxis("Horizontal");
        float newX = transform.position.x + Time.deltaTime * speed * dir;

        float playAreaSize = playArea.localScale.x * 10;
        float paddleSize = transform.localScale.x * 1;
        
        float maxX = 0.5f * playAreaSize - 0.5f * paddleSize;
        float clampX = Mathf.Clamp(newX, -maxX, maxX);

        //transform.position += new Vector3(Time.deltaTime*speed*dir, 0, 0);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
    }
}
