using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject[] prefabs;
    public Transform[] background;
    public float[] length;

    private Vector3 direction;
    float first;
    float second;


    void Start()
    {
        direction = new Vector3(1, 0, 0);
        first = length[0];
        second = length[1];
        background[0] = Instantiate(prefabs[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform).transform;
        background[1] = Instantiate(prefabs[1], new Vector3(transform.position.x - length[0], transform.position.y, transform.position.z), Quaternion.identity, transform).transform;
    }

    // Update is called once per frame
    void Update()
    {
        positionUpdate();
        checkPosition(first, second);
    }

    private void checkPosition(float first, float second)
    {
        if (background[0].position.x >= first)
        {
            moveToTop(0);
        }
        if (background[1].position.x >= second)
        {
            moveToTop(1);
        }
    }

    private void moveToTop(int index)
    {
        if (index == 0)
        {
            background[0].position = new Vector3(transform.position.x-length[0], transform.position.y, transform.position.z);
        }
        else if (index == 1)
        {
            background[1].position = new Vector3(transform.position.x-length[1], transform.position.y, transform.position.z);
        }
    }



    private void positionUpdate()
    {
        background[0].position += direction * Time.deltaTime * speed;
        background[1].position += direction * Time.deltaTime * speed;
    }
}
