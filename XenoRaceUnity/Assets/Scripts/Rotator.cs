using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    public enum Axes
    {
        x,
        y,
        z
    }
    public Axes spinDirection = Axes.y;
    public float Speed;
    public float HoverSpeed;
    public float HoverTimeout;
    private float TimeSinceChange = 0f;
    private bool GoingUp = false;
	
	// Update is called once per frame
	void Update ()
    {
		switch (spinDirection)
        {
            case Axes.x:
                transform.Rotate(Speed * Time.deltaTime,0f, 0f);
                break;
            case Axes.y:
                transform.Rotate(0f, Speed * Time.deltaTime, 0f);
                break;
            case Axes.z:
                transform.Rotate(0f, 0f, Speed * Time.deltaTime);
                break;
        }
        TimeSinceChange += Time.deltaTime;
        if (GoingUp)
            transform.position += new Vector3(0f, HoverSpeed * Time.deltaTime, 0f);
        else
            transform.position -= new Vector3(0f, HoverSpeed * Time.deltaTime, 0f);
        if (TimeSinceChange > HoverTimeout)
        {
            GoingUp = !GoingUp;
            TimeSinceChange = 0f;
        }
    }
}
