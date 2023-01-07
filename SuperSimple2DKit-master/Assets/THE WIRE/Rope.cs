using UnityEngine;
using System.Collections.Generic;

public class Rope : MonoBehaviour
{

	public Rigidbody2D hook;

	public GameObject linkPrefab;

	public int links = 7;

	public UnityEngine.LineRenderer lr;

	public List<Transform> transforms;

	void Start()
	{
		transforms = new List<Transform>();
		GenerateRope();
		
	}

    private void Update()
    {
		RenderRope();
	}

    void GenerateRope()
	{
		Rigidbody2D previousRB = hook;
		transforms.Add(hook.transform);
		for (int i = 0; i < links; i++)
		{
			GameObject link = Instantiate(linkPrefab, transform);
			link.transform.parent = transform;
			link.transform.position = transform.position;
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousRB;

			previousRB = link.GetComponent<Rigidbody2D>();
			transforms.Add(link.transform);

		}
	}

	void RenderRope()
    {
		Vector3[] ps = new Vector3[transforms.Count];
		lr.positionCount = transforms.Count;
        for(int i=0; i<transforms.Count;i++)
        {
			ps[i] = transforms[i].position;
        }
		lr.SetPositions(ps);
    }

}