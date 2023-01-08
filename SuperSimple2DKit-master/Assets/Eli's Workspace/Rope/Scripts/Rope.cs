using UnityEngine;
using System.Collections.Generic;

public class Rope : MonoBehaviour
{

	public Rigidbody2D hook;

	public GameObject hookPrefab;

	public GameObject linkPrefab;

	public int links = 2;

	public LineRenderer lr;

	public List<Transform> transforms;

	public RopeSegment secondSegmentCache;

	private List<Pylon> attachedPylons = new List<Pylon>();

	public RopeSegment lastConnectedRopeSegment;

	public Electricity copyLine;

	void Start()
	{
		transforms = new List<Transform>();
		GenerateRope();
	}

    private void Update()
    {
		RenderRope();
		float d = Vector2.Distance(transforms[0].position, transforms[1].position);
		float d2 = Vector2.Distance(transforms[1].position, transforms[2].position);
		if (d > 1 || d2 > 2)
        {
			AddHook();
        }
		else if (d2 < 1f && secondSegmentCache.deletable)
        {
			DeleteNearestSegment();
        }
	}

    void GenerateRope()
	{
		Rigidbody2D previousRB = hook;
		transforms.Add(hook.transform);
		for (int i = 0; i < links; i++)
		{
			GameObject link = Instantiate(linkPrefab, transform);
			link.transform.position = transform.position;
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousRB;

			previousRB = link.GetComponent<Rigidbody2D>();
			transforms.Add(link.transform);
			link.GetComponent<RopeSegment>().undeletable = true;
		}
		secondSegmentCache = transforms[1].GetComponent<RopeSegment>();

		transforms[^1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		
	}

	void AddHook()
    {
		GameObject newHook = Instantiate(hookPrefab, transform);
		newHook.transform.position = hook.transform.position;
		GameObject link = Instantiate(linkPrefab, transform);
		link.transform.position = transforms[1].position;
		Rigidbody2D rb = newHook.GetComponent<Rigidbody2D>();
		link.GetComponent<HingeJoint2D>().connectedBody = rb;
		HookFollow newHookFollow = newHook.GetComponent<HookFollow>();
		HookFollow oldHookFollow = hook.GetComponent<HookFollow>();
		newHookFollow.follow = oldHookFollow.follow;
		transforms[1].GetComponent<HingeJoint2D>().connectedBody = link.GetComponent<Rigidbody2D>();
		transforms[1].GetComponent<RopeSegment>().Inst();
		Destroy(hook.gameObject);
		hook = rb;
		transforms[0] = link.transform;
		transforms.Insert(0, newHook.transform);
		secondSegmentCache = transforms[1].GetComponent<RopeSegment>();
        foreach (var t in transforms)
        {
			if (t == transforms[0]) continue;
			t.GetComponent<Rigidbody2D>().mass += .01f;
        }
	}

	void DeleteNearestSegment()
    {
		Transform nearest = transforms[1];
		transforms.RemoveAt(1);
		Destroy(nearest.gameObject);
		transforms[1].GetComponent<HingeJoint2D>().connectedBody = hook.GetComponent<Rigidbody2D>();
		transforms[1].GetComponent<RopeSegment>().Inst();
		secondSegmentCache = transforms[1].GetComponent<RopeSegment>();
		secondSegmentCache.deletable = false;
		StartCoroutine(secondSegmentCache.CountdownToDeletable(.001f));
		foreach (var t in transforms)
		{
			if (t == transforms[0]) continue;
			t.GetComponent<Rigidbody2D>().mass -= .01f;
		}
		//transforms[transforms.Count - 1 + links].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
	}

	void RenderRope()
    {
		Vector3[] ps = new Vector3[transforms.Count];
		lr.positionCount = transforms.Count;
        for(int i=0; i<transforms.Count;i++)
        {
			ps[i] = transforms[i].position;
        }
		Vector3[] positions = new Vector3[lr.positionCount];
		lr.GetPositions(positions);
		for(int i = 0; i < lr.positionCount-1; i++)
        {
			ps[i] = Vector2.Lerp(positions[i], ps[i], .7f);
        }
		lr.SetPositions(ps);
    }

	public RopeSegment AttachDetachPylon(Pylon p)
    {
		//secondSegmentCache.transform.position = t.position;
		if(!attachedPylons.Contains(p))
        {
			AddHook();
			secondSegmentCache.undeletable = true;
			secondSegmentCache.transform.position = p.transform.position;
			secondSegmentCache.transform.rotation = secondSegmentCache.connectedBelow.transform.rotation;
			secondSegmentCache.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			attachedPylons.Add(p);
			lastConnectedRopeSegment = secondSegmentCache;
			return secondSegmentCache;
		}
		else
		{
			RopeSegment toDetach = p.attachedRopeSegment;
			toDetach.undeletable = false;
			toDetach.deletable = true;
			toDetach.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			attachedPylons.Remove(p);
			if (attachedPylons.Count > 0)
				lastConnectedRopeSegment = attachedPylons[^1].attachedRopeSegment;
			else
				lastConnectedRopeSegment = null;
			return null;
		}

	}

}