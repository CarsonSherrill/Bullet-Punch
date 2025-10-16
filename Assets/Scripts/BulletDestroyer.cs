using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public float lifeTime = 3f;

    private TextController textController;

    void Start()
    {
        textController = GameObject.Find("Timer").GetComponent<TextController>();
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            textController.addScore();
        }
        Destroy(gameObject);
    }
}