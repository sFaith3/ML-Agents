using MLAgents;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Penguin
{
    /// <summary>
    /// Class that manages the training area containing one penguin, one baby penguin
    /// and several fish. It spawns and removes fish, and also places the penguins
    /// in random locations.
    /// </summary>
    public class PenguinArea : MonoBehaviour
    {
        [Tooltip("The agent inside the area")]
        public PenguinAgent PenguinAgent;

        [Tooltip("The baby penguin inside the area")]
        public GameObject PenguinBaby;

        [Tooltip("The TextMeshPro text that shows the cumulative reward of the agent")]
        public TextMeshPro CumulativeRewardText;

        [Tooltip("Prefab of a live fish")]
        public Fish FishPrefab;

        private List<GameObject> fishList;

        /// <summary>
        /// Called when the game starts
        /// </summary>
        private void Start()
        {
            ResetArea();
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        private void Update()
        {
            // Update the cumulative reward text
            CumulativeRewardText.text = PenguinAgent.GetCumulativeReward().ToString("0.00");
        }

        /// <summary>
        /// Reset the area, including fish and penguin placement
        /// </summary>
        public void ResetArea()
        {
            RemoveAllFish();
            PlacePenguin();
            PlaceBaby();
            SpawnFish(4, Academy.Instance.FloatProperties.GetPropertyWithDefault("fish_speed", 0.5f));
        }

        /// <summary>
        /// Remove a specific fish from the area when it is eaten
        /// </summary>
        /// <param name="fishObject">The fish to remove</param>
        public void RemoveSpecificFish(GameObject fishObject)
        {
            fishList.Remove(fishObject);
            Destroy(fishObject);
        }

        /// <summary>
        /// The number of fish remaining
        /// </summary>
        public int FishRemaining
        {
            get { return fishList.Count; }
        }

        /// <summary>
        /// Choose a random position on the X-Z plane within a partial donut shape
        /// </summary>
        /// <param name="center">The center of the donut</param>
        /// <param name="minAngle">Minimum angle of the wedge</param>
        /// <param name="maxAngle">Maximum angle of the wedge</param>
        /// <param name="minRadius">Minimum distance from the center</param>
        /// <param name="maxRadius">Maximum distance from the center</param>
        /// <returns>A position falling within the specified region</returns>
        public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
        {
            float radius = minRadius;
            float angle = minAngle;

            if (maxRadius > minRadius)
            {
                // Pick a random radius
                radius = Random.Range(minRadius, maxRadius);
            }

            if (maxAngle > minAngle)
            {
                // Pick a random angle
                angle = Random.Range(minAngle, maxAngle);
            }

            // Center position + forward vector rotated around the Y axis by "angle" degrees, multiplies by "radius"
            return center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
        }

        /// <summary>
        /// Remove all fish from the area
        /// </summary>
        private void RemoveAllFish()
        {
            if (fishList != null)
            {
                for (int i = 0; i < fishList.Count; i++)
                {
                    if (fishList[i] != null)
                    {
                        Destroy(fishList[i]);
                    }
                }
            }

            fishList = new List<GameObject>();
        }

        /// <summary>
        /// Place the penguin in the area
        /// </summary>
        private void PlacePenguin()
        {
            Rigidbody rigidbody = PenguinAgent.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            PenguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
            PenguinAgent.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }

        /// <summary>
        /// Place the baby in the area
        /// </summary>
        private void PlaceBaby()
        {
            Rigidbody rigidbody = PenguinBaby.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            PenguinBaby.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * .5f;
            PenguinBaby.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        /// <summary>
        /// Spawn some number of fish in the area and set their swim speed
        /// </summary>
        /// <param name="count">The number to spawn</param>
        /// <param name="fishSpeed">The swim speed</param>
        private void SpawnFish(int count, float fishSpeed)
        {
            for (int i = 0; i < count; i++)
            {
                // Spawn and place the fish
                GameObject fishObject = Instantiate<GameObject>(FishPrefab.gameObject);
                fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
                fishObject.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                // Set the fish's parent to this area's transform
                fishObject.transform.SetParent(transform);

                // Keep track of the fish
                fishList.Add(fishObject);

                // Set the fish speed
                fishObject.GetComponent<Fish>().FishSpeed = fishSpeed;
            }
        }
    }
}