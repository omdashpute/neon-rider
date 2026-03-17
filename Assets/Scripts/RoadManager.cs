using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Transform player;
    public Transform[] segments;
    public float segmentLength = 30f;

    int currentIndex = 0;

    void Update()
    {
        if (player.position.z > segments[currentIndex].position.z + segmentLength)
        {
            MoveSegment();
        }
    }

    void MoveSegment()
    {
        Transform seg = segments[currentIndex];

        float farthestZ = segments[0].position.z;

        foreach (Transform s in segments)
            if (s.position.z > farthestZ) farthestZ = s.position.z;

        seg.position = new Vector3(0, 0, farthestZ + segmentLength);

        currentIndex = (currentIndex + 1) % segments.Length;
    }
}