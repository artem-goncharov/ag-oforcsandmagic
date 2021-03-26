using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathingManage : MonoBehaviour
{
    WaveConfigManage waveConfig;

    List<Transform> waypoints;
    int waypointIndex = 0;
    int waypointsCount;    

    public void SetWaveConfig(WaveConfigManage waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        //Debug.Log(waypoints);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        waypointsCount = waypoints.Count;

        if (waypointIndex <= waypointsCount - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
            //Debug.Log(waypointIndex);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
    }
}
