using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject guide;
    public GameObject player;
    public Material start;
    public Material goal;
    public int n, m;

    void Start()
    {
        System.Random random = new System.Random();
        int startIndex = random.Next(m);
        int goalIndex = random.Next(m);

        guide.transform.localScale = new Vector3(10 / m, 1, 10 / n);

        guide.transform.position = new Vector3((goalIndex * 100) / n - 50 + guide.transform.localScale.z * 5, 1, (((n - 1) * 100) / n + guide.transform.localScale.z * 5 - 50));

        guide.GetComponent<Renderer>().material = goal;

        Instantiate(guide);

        guide.transform.localScale = new Vector3(10 / m, 1, 10 / n);

        guide.transform.position = new Vector3((startIndex * 100) / n - 50 + guide.transform.localScale.z * 5, 1, (guide.transform.localScale.z * 5 - 50));

        guide.GetComponent<Renderer>().material = start;

        Instantiate(guide);

        player.transform.position = guide.transform.position;
        player = Instantiate(player);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
                player.GetComponent<Rigidbody>().AddForce(Vector3.forward * 2);
            if (Input.GetKey(KeyCode.A))
                player.GetComponent<Rigidbody>().AddForce(Vector3.left * 2);
            if (Input.GetKey(KeyCode.S))
                player.GetComponent<Rigidbody>().AddForce(Vector3.back * 2);
            if (Input.GetKey(KeyCode.D))
                player.GetComponent<Rigidbody>().AddForce(Vector3.right * 2);
        }
    }
}
