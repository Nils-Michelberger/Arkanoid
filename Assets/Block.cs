using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int lives;
    public GameObject normalBlock;
    public GameObject crackedBlock;
    public GameObject brokenBlock;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        lives -= 1;

        switch (lives)
        {
            case 2:
                Destroy(normalBlock);
                crackedBlock.SetActive(true);
                break;
            case 1:
                Destroy(crackedBlock);
                brokenBlock.SetActive(true);
                break;
            default:
                Destroy(brokenBlock);
                Destroy(gameObject);
                break;
        }
    }
}