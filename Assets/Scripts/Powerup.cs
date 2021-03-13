using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ToggleVisibilityAndCollider(false);
        StartTimerForPowerUp();
    }

    private IEnumerator ShowAfterRandomTime()
    {
        yield return new WaitForSeconds(Random.value * 15 + 2.5f);
        ToggleVisibilityAndCollider(true);
    }

    public void StartTimerForPowerUp()
    {
        StartCoroutine(ShowAfterRandomTime());
    }
    
    protected void ToggleVisibilityAndCollider(bool visibleAndHittable)
    {
        //NOTE: Merge all meshes into one to increase performance
        GetComponent<BoxCollider>().enabled = visibleAndHittable;
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = visibleAndHittable;
    }
}
