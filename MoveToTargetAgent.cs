using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System.Data.Common;
using Unity.MLAgents.Sensors;
using UnityEngine.UIElements.Experimental;
using UnityEditor.Experimental.GraphView;

public class MoveToTarget : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private MeshRenderer backgroundRenderer;
    [SerializeField] private Transform env;
    [SerializeField] private GameObject obstacle; // The wall prefab
    [SerializeField] private int numberOfObstacles = 1; // Number of walls to spawn

    private float episodeTimer; // Timer for the episode duration
    private float episodeDuration = 5f; // Duration of the episode in seconds

    public override void OnEpisodeBegin()
    {
        episodeTimer = 0f; // Reset the timer at the beginning of the episode

        // Randomize the agent's and target's positions relative to their own environment
        transform.localPosition = new Vector3(Random.Range(-8f, 8f), 0.5f, Random.Range(-10f, 10f));
        target.localPosition = new Vector3(Random.Range(-8f, 8f), 0.5f, Random.Range(-10f, 10f));

        env.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = Quaternion.identity;

        // Clear existing walls in this environment only
        foreach (Transform child in env)
        {
            if (child.CompareTag("Obstacle"))
            {
                Destroy(child.gameObject);
            }
        }

        // Randomize the number of obstacles between 3 and 5
        numberOfObstacles = Random.Range(1, 6);

        // Spawn new walls in this environment
        for (int i = 0; i < numberOfObstacles; i++)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        // Generate a random position relative to the environment
        Vector3 randomLocalPosition = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-10f, 10f));

        // Use the environment's rotation
        Quaternion randomRotation = env.rotation;

        // Create an obstacle at the random local position relative to the environment
        GameObject newObstacle = Instantiate(obstacle, env.TransformPoint(randomLocalPosition), randomRotation, env);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector3)transform.localPosition);
        sensor.AddObservation((Vector3)target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float movementSpeed = 5f;

        transform.localPosition += new Vector3(moveX, 0f, moveZ) * Time.deltaTime * movementSpeed;

        // Update the timer
        episodeTimer += Time.deltaTime;

        // Check if the episode should end due to time limit
        if (episodeTimer >= episodeDuration)
        {
            AddReward(-2f); // Add a negative reward
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Target>(out Target target))
        {
            AddReward(10f);
            backgroundRenderer.material.color = Color.green;
            EndEpisode();
        }
        else if (collision.TryGetComponent<Wall>(out Wall wall) || collision.CompareTag("Obstacle"))
        {
            AddReward(-2f);
            backgroundRenderer.material.color = Color.red;
            EndEpisode();
        }
    }
}
