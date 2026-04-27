using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // Reference to the player transform and road segments
    public Transform player;
    public Transform[] segments;
    public float segmentLength = 30f;

    // Index to track the current segment being moved
    int currentIndex = 0;

    void Update()
    {
        // Check if the player has moved past the current segment and move it if necessary
        if (player.position.z > segments[currentIndex].position.z + segmentLength)
        {
            MoveSegment();
        }
    }

    void MoveSegment()
    {
        Transform seg = segments[currentIndex]; // Get the current segment to move

        float farthestZ = segments[0].position.z; // Initialize farthestZ to the position of the first segment

        // Loop through all segments to find the farthest z position
        foreach (Transform s in segments)
            if (s.position.z > farthestZ) farthestZ = s.position.z;

        seg.position = new Vector3(0, 0, farthestZ + segmentLength); // Move the current segment to the end of the road by setting its position to be farthestZ + segmentLength

        currentIndex = (currentIndex + 1) % segments.Length; // Update the current index to the next segment, wrapping around if it exceeds the number of segments
    }
}