using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DwarfAgent : Agent
{
    Rigidbody2D rBody;
    public float speedMultiplier = 10.0f;

    private Vector2 initialpos;
    private Vector2 goblinpos1;
    private Vector2 goblinpos2;
    private Vector2 goblinpos3;

    public GameObject goblin1;
    public GameObject goblin2;
    public GameObject goblin3;
    public GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        initialpos = this.transform.position;
        goblinpos1 = goblin1.transform.position;
        goblinpos2 = goblin2.transform.position;
        goblinpos3 = goblin3.transform.position;

    }



    public override void OnEpisodeBegin()
    {
        rBody.velocity = Vector2.zero;
        transform.position = initialpos;
        goblin1.transform.position = goblinpos1;
        goblin2.transform.position = goblinpos2;
        goblin3.transform.position = goblinpos3;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation((float)StepCount/MaxStep);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);

        // the goal position
        sensor.AddObservation(goal.transform.localPosition);
        //where the goblin is
        sensor.AddObservation(goblin1.transform.localPosition);
        sensor.AddObservation(goblin2.transform.localPosition);
        sensor.AddObservation(goblin3.transform.localPosition);


    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector2 forceDirection = Vector2.zero;
        switch (actionBuffers.DiscreteActions[0]) {
            case 0:
                forceDirection = Vector2.zero;
                break;
            case 1:
                forceDirection = new Vector2(1.0f, 0.0f);
                break;
            case 2:
                forceDirection = new Vector2(1.0f, 0.0f);
                break;
            case 3:
                forceDirection = new Vector2(0.0f, 1.0f);
                break;
            case 4:
                forceDirection = new Vector2(0.0f, -1.0f);
                break;
            case 5:
                forceDirection = new Vector2(1.0f, 1.0f);
                break;
            case 6:
                forceDirection = new Vector2(1.0f, -1.0f);
                break;
            case 7:
                forceDirection = new Vector2(-1.0f, 1.0f);
                break;
            case 8:
                forceDirection = new Vector2(-1.0f, -1.0f);
                break;
            default:
                forceDirection = Vector2.zero;
                break;
        }

        rBody.AddForce(forceDirection * speedMultiplier);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Goal")
        {
            AddReward(1f);
            EndEpisode();
        } else if(col.tag == "Goblin")
        {
            EndEpisode();
        }
    }

}
