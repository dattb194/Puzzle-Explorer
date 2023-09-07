//---------------------------------------------------------------------------------------------------------------
//This is a little program of Draw lines with physics. Enjoy!
//MouseDraw v1.0 by Unicorn
//---------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

public class DrawingPhysics : MonoBehaviour {

	public List<GameObject> linesPrefab;
    public List<GameObject> collidersPrefab;
    public List<Material> lineMaterials;

    public GameObject linePrefab;
	public Material lineMaterial;
	public GameObject colliderPrefab;
	private GameObject colliderClone;
	public float width = 1;
	private Camera cameras;
	private GameObject brushDrawingClone;
	private LineRenderer lineRenderer;
	private Vector3 initdrawPos;
	private Vector3 lastdrawPos;
	private Vector3 drawPos;
	private int vertexCount;
	[SerializeField] GameObject newLineMesh;
	private float cameraFar=20;
	private bool startDraw = false;
	private bool endDraw = false;
	public float destroyTime = 10;
	private GameObject lineCollection;
	public bool drawStable = false;
	public bool isDrawing;

	[SerializeField] DrawState stateDraw;
    public DrawState StateDraw
	{
		get => stateDraw;
		set
		{
			stateDraw = value;
			linePrefab = linesPrefab[((int)value)];
			colliderPrefab = collidersPrefab[(int)value];
			lineMaterial = lineMaterials[(int)value];
			_SetDrawStable((int)value);
		}
	}

    void Start (){
		//Find MainCamera
		if (cameras==null && GameObject.FindWithTag("MainCamera"))
		{
			cameras = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		cameraFar = Mathf.Abs(cameras.transform.position.z-transform.position.z);
		
		lineCollection = GameObject.Find("PhysicLines");
		if (lineCollection == null)
		{
			lineCollection = new GameObject("PhysicLines");
		}
	}
    private void Update()
    {
		Draw();
    }
    void Draw ()
	{
		if (StateDraw == DrawState.none) return;

		#if UNITY_EDITOR  ||  UNITY_STANDALONE  ||  UNITY_WEBPLAYER 
			startDraw = Input.GetKeyDown(KeyCode.Mouse0);
			endDraw = Input.GetKeyUp(KeyCode.Mouse0);
		#else  // For touch-devices
			if (Input.touchCount > 0) 
			{
				Touch touch = Input.GetTouch (0);
				startDraw = (touch.phase == TouchPhase.Began);
				endDraw = (touch.phase == TouchPhase.Ended);
			}
		#endif

		//When Start Draw
		if (startDraw)
		{
			if (isDrawing==false)
			{
				//Get first point
				initdrawPos=cameras.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraFar));
				//Create LineRenderer gameobject
				brushDrawingClone = Instantiate(linePrefab, initdrawPos, cameras.transform.rotation) as GameObject;
				lineRenderer = brushDrawingClone.GetComponent<LineRenderer>();
				lastdrawPos = initdrawPos;
				//Set drawing flag
				isDrawing = true;
				vertexCount = 1;
				//Create a new GameObject to be parent of LineRenderer and it's colliders
				newLineMesh = new GameObject ("newline");
				lineCollection = GameObject.Find("PhysicLines");
				if (lineCollection == null)
				{
					lineCollection = new GameObject("PhysicLines");
				}
				newLineMesh.transform.position = transform.position;
				newLineMesh.transform.SetParent(lineCollection.transform);
			}
		}
		//When End Draw
		if (endDraw)
		{
			//Set drawing flag
			isDrawing = false;
			//Set LineRenderer gameobject to be child of newLineMesh
			brushDrawingClone.transform.parent = newLineMesh.transform;
			//Add Rigidbody to newLineMesh
			Rigidbody newRigbody = newLineMesh.AddComponent<Rigidbody>();
			
			newRigbody.interpolation = RigidbodyInterpolation.Interpolate;
			newRigbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            //Rigdbody constraints
            newRigbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
			//Hold leftShift to draw soild lines
			if (drawStable)
			{
				newRigbody.isKinematic = true;
			}

			//Add delay destroy
			Destroy(newLineMesh, destroyTime);
			switch (StateDraw)
			{
				case DrawState.brick:
					Destroy(newLineMesh.gameObject, 3);
					break;
				case DrawState.fire:
				case DrawState.water:
                    Destroy(newLineMesh.gameObject, 1);
					break;
            }

			if (StateDraw == DrawState.rope || StateDraw == DrawState.fire || StateDraw == DrawState.water || StateDraw == DrawState.brick)
			{
				for (int i = 0; i < newLineMesh.transform.childCount - 1; i++)
				{
					newLineMesh.transform.GetChild(i).GetComponent<LineElement>().Active();
				}
			}

			StateDraw = DrawState.none;
		}
		
		//Set LineRenderer and create it's colliders
		if (isDrawing && brushDrawingClone && mouseMove() && GPMng.inst.Enegy>0)
		{
			//Screen to WorldPoint
			drawPos = cameras.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraFar));
			//Calculate distance between this draw point and last
			float drawdistance = Vector3.Distance(drawPos, lastdrawPos);
			//When draw a new point which far enough
			if (drawdistance>=cameraFar*width/80)
			{
				//Add a new LineRenderer node
				vertexCount = vertexCount + 1;
				lineRenderer.positionCount = vertexCount;
				lineRenderer.SetPosition(vertexCount-1, brushDrawingClone.transform.InverseTransformPoint(drawPos));
				lineRenderer.startWidth = cameraFar*width/60;
				lineRenderer.endWidth = cameraFar*width/60;
				lineRenderer.sharedMaterial = lineMaterial;
				//Create colliders belongs to it
                colliderClone = Instantiate(colliderPrefab, (drawPos+lastdrawPos)/2, Quaternion.LookRotation(drawPos-lastdrawPos)) as GameObject;

				CapsuleCollider capsulecollider = colliderClone.GetComponent<CapsuleCollider>();
				capsulecollider.radius = cameraFar*width/120;
				capsulecollider.height = drawdistance;
				//Make colliders to be childs of newLineMesh
				colliderClone.transform.parent = newLineMesh.transform;
				//Save last draw point
				lastdrawPos = drawPos;
				GPMng.inst.Enegy--;
			}
		}
		
	}
	int stateSelect;
	public void SetStateDraw(int state)
	{
		stateSelect = state;

        Invoke(nameof(_SetStateDraw), .2f);
	}
    bool mouseMove()
    {
        if (Input.GetAxis("Mouse X") != 0) return true;
        if (Input.GetAxis("Mouse Y") != 0) return true;
        return false;
    }
    void _SetStateDraw()
    {
        switch (stateSelect)
        {
            case 1:
                StateDraw = DrawState.walk;
                break;
            case 2:
                StateDraw = DrawState.water;
                break;
            case 3:
                StateDraw = DrawState.fire;
                break;
            case 4:
                StateDraw = DrawState.rope;
                break;
            case 5:
                StateDraw = DrawState.brick;
                break;
        }
    }
	void _SetDrawStable(int stateSelect)
	{
        switch (stateSelect)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                drawStable = true;
                break;
            case 5:
                drawStable = false;
                break;
        }
    }

    public void SetStable()
	{
		drawStable = !drawStable;
	}

	public void ClearLines()
	{
		Destroy(lineCollection);
	}
}
public enum DrawState
{ 
	none = 0, walk = 1, water = 2, fire = 3, rope = 4, brick = 5
}
