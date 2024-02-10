using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;

public class PistonScript : MonoBehaviour
{

    public float moveSpeed = 1;
    public float pistonTimer = 0;
    public float pistonTime = 0.1f; //speed of growth
    public float waitTimer = 0;
    public float waitTime = 2f; //speed of growth
    public bool isUpwards, isDownwards;
    public Vector2 topPosition;
    public Vector2 bottomPosition;

    public GameObject Paths;
    public float pointCount;
    public int pointIndex;
    public Transform[] wayPoints;
    Vector3 targetPos;
    int direction = 1;
    // Start is called before the first frame update

    private void Awake()
    {
        wayPoints = new Transform[Paths.transform.childCount];
        for(int i = 0; i < Paths.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = Paths.transform.GetChild(i).gameObject.transform;
        }
    }
    void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;

        bottomPosition = transform.position;
        targetPos = wayPoints[pointIndex].transform.position;
        topPosition = new Vector2(transform.position.x, transform.position.y+1);
        isUpwards = true;
        isDownwards = false;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos,moveSpeed * Time.deltaTime);

        if(transform.position == targetPos )
        {
            NextPoint();
        }
        //if (isDownwards) { StartCoroutine(PistonDown()); }
        //if (isUpwards) { StartCoroutine(PistonUp()); }
    }

    void NextPoint()
    {
        if (pointIndex ==pointCount - 1 )
        {
            direction = -1;
        }

        if (pointIndex == 0)
        {
            direction=1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    // Update is called once per frame
    public IEnumerator PistonUp()
    {
        yield return new WaitForSeconds(moveSpeed);
        while (isUpwards) {
            do
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                pistonTimer += Time.deltaTime;
                yield return null; 
            }
            while (transform.position.y < topPosition.y);
            //yield return new WaitForSeconds(2);
            pistonTimer = 0;
            do
            {
                waitTimer += Time.deltaTime;
                yield return null;
            }
            while (waitTimer < waitTime);
            waitTimer = 0;

            isUpwards = false;
            isDownwards = true;
        }

    }

    public IEnumerator PistonDown()
    {
        while (isDownwards)
        {
            do
            {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime;
                pistonTimer += Time.deltaTime;
                yield return null;
            }
            while (transform.position.y > bottomPosition.y);
            pistonTimer = 0;
            //yield return new WaitForSeconds(2);
            do
            {
                waitTimer += Time.deltaTime;
                yield return null;
            }
            while (waitTimer < waitTime);
            waitTimer = 0;

            isUpwards = true;
            isDownwards = false;
        }

    }
}
