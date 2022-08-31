using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    // Zincirler arasi baglanti mesafeleri. Uzunluklara gore degismeli
    [SerializeField] private float distanceHookValue;

    // Her bir zinciri farkli keylerde tutarak. farklý noktalardaki baglanti kontrolunu kolaylaþtýrdýk.
    public Dictionary<string, HingeJoint2D> hingeController = new Dictionary<string, HingeJoint2D>(); 

    public void LastConnectRope(Rigidbody2D rigidbody2D, string centerName)
    {
        HingeJoint2D joint2D = gameObject.AddComponent<HingeJoint2D>();
        hingeController.Add(centerName, joint2D);
        joint2D.autoConfigureConnectedAnchor = false; // Otomatik kontrolu genelde devre disi birakiriz
        joint2D.connectedBody = rigidbody2D;
        joint2D.anchor = Vector2.zero;
        joint2D.connectedAnchor = new Vector2(0, -distanceHookValue);
    }
}
