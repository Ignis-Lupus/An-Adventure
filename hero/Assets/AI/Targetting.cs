using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{

    public List<Transform> targets;
    public Transform selectedTarget;
    public string targetTag = "Human";
    private Transform myTransform;

    //Use this for initialization
    void Start()
    {
        targets = new List<Transform>();
        selectedTarget = null;
        myTransform = transform;
        AddAllEnemies();
    }

    public void AddAllEnemies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject Human in go)
        {
            AddTarget(Human.transform);
        }
    }

    public void AddTarget(Transform Human)
    {
        targets.Add(Human);
    }

    private void SortTargetsByDistance()
    {
        targets.Sort(delegate (Transform t1, Transform t2) {
            return (Vector3.Distance(t1.position, myTransform.position).CompareTo)
                (Vector3.Distance(t2.position, myTransform.position));
        });
    }

    public void TargetEnemy()
    {
        if (selectedTarget == null)
        {
            SortTargetsByDistance();
            selectedTarget = targets[0];
        }
        else
        {
            int index = targets.IndexOf(selectedTarget);
            if (index < targets.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }

            selectedTarget = targets[index];
        }
    }

    //Update is called once per frame
    void Update()
    {

        TargetEnemy();
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
            
        //}
    }
}
