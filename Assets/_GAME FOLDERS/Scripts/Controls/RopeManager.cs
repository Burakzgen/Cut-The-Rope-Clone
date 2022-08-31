using UnityEngine;

public class RopeManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D firstHook;
    [SerializeField] private Ball ball;

    [SerializeField] private int connectCount = 5;

    public GameObject[] connectObjects;

    public string centerName;

    private void Start()
    {
        SpawnRopeConnect();
    }

    private void SpawnRopeConnect()
    {
        Rigidbody2D prevConnect = firstHook;

        for (int i = 0; i < connectCount; i++)
        {
            connectObjects[i].SetActive(true);
            HingeJoint2D hingeJoint2D = connectObjects[i].GetComponent<HingeJoint2D>();
            hingeJoint2D.connectedBody = prevConnect;

            if (i < connectCount - 1)
            {
                prevConnect = connectObjects[i].GetComponent<Rigidbody2D>();
            }
            else
            {
                ball.LastConnectRope(connectObjects[i].GetComponent<Rigidbody2D>(), centerName); // Son baglanti noktasi
            }

        }
    }
}
