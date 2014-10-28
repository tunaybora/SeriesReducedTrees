using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class InputManager : MonoBehaviour {


	public GameObject Node;
	public GameObject NodeCollider;
	public GUIText NCount;
	public GUIText CheckTree;

	public float distance = -10;
	private bool FirstTap;
	private bool TappedOnNode;
	private int NCountInt;
	private Ray MouseRay;
	private RaycastHit MouseRayHit;

	//DRAW LINE
	public GameObject LineRendererGameObject;
	private LineRenderer LineRendererComponent;
	private GameObject GameObjectHiten;
	private Vector3 LineMouse;
	private float counter;
	private float dist;
	private int TapCount;
	private float LineMouseZ;
	
	private float TappedNodePosX;
	private float TappedNodePosY;

	private bool ClearConnection;
	private bool NewNode;

	public float lineDrawSpeed = 6f;

	private GameObject[] NodeColliders;

	private float LineLength;

	private RaycastHit NotExactCennectionHit;

	private bool IsHittenNotExactConnection;

	private bool MadeConnection;


	void Start () {

		QualitySettings.antiAliasing = 8;
		Node.renderer.enabled = false;
		CheckTree.enabled = false;
		Node.GetComponent<SphereCollider>().enabled = false;

		//DRAW LINE
		LineRendererComponent = LineRendererGameObject.GetComponent<LineRenderer>();
		LineRendererComponent.SetColors(Color.white, Color.white);
		LineMouseZ = 1.299988f;

		string NCountString = NCount.text;
		string NCountStringResult = Regex.Match(NCountString, @"\d+").Value;
		NCountInt = int.Parse(NCountStringResult);
	}

	void Update () {

		//**************************************************************************
		// Create a Node
		//**************************************************************************

		Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 Point = MouseRay.origin + (MouseRay.direction);

		//Ray Hit
		bool IsHitten = Physics.Raycast(MouseRay, out MouseRayHit, 100f);
		Debug.DrawRay(MouseRay.origin, MouseRay.direction*100);
		
		if(TapCount<=NCountInt){

			if(!TappedOnNode){

				//First Tap - Delete Instruction
				if(Input.GetMouseButtonDown(0) && !FirstTap){
					FirstTap = true;
					Destroy(GameObject.Find("TapToStart"));
					TapCount++;
					Node.GetComponent<SphereCollider>().enabled = true;
					CheckTree.enabled = true;
				}

				//Create Node
				if(Input.GetMouseButtonDown(0) && FirstTap && !ClearConnection && !IsHitten && !ClearTree.ClearedTree){
					Node.renderer.enabled = true;
					Node.transform.position = Point;
					Instantiate(Node, Point, Quaternion.identity);
					TapCount++;
					NewNode = true;
				}

			}

		}

		//**************************************************************************
		// Tapped On Node
		//**************************************************************************

		if(IsHitten && Input.GetMouseButtonDown(0) && MouseRayHit.collider.tag == "Node"){
			if(!NewNode){
				//Create Connector
				TappedOnNode = true;
				//Gameobject Hiten
				GameObjectHiten = MouseRayHit.transform.gameObject;
			}
		}

		else{
			TappedOnNode = false;
		}
		
		if(TapCount != 0){
			LineMouse = new Vector3(MouseRay.origin.x,MouseRay.origin.y, LineMouseZ);
			LineRendererComponent.SetPosition(1, LineMouse);
		}

		if(TappedOnNode && !NewNode && GameObjectHiten.tag == "Node"){
			LineRendererComponent.SetPosition(0, GameObjectHiten.transform.position);
			TappedNodePosX = GameObjectHiten.transform.position.x;
			TappedNodePosY = GameObjectHiten.transform.position.y;
			LineRendererComponent.enabled = true;
			ClearConnection = true;
		}

		//**************************************************************************
		// Clear Connection
		//**************************************************************************

		if(Input.GetMouseButtonDown(0) && !TappedOnNode){
			LineRendererComponent.enabled = false;
			ClearConnection = false;
			NewNode = false;
		}

		//**************************************************************************
		// Create Connection
		//**************************************************************************
		if(ClearConnection){
			LineLength = Mathf.Sqrt(Mathf.Abs((Mathf.Pow(Point.x,2)-Mathf.Pow(GameObjectHiten.transform.position.x,2))+(Mathf.Pow(Point.y,2)-Mathf.Pow(GameObjectHiten.transform.position.y,2))));

			//Disable all BoxColliders
			NodeColliders = GameObject.FindGameObjectsWithTag("NodeCollider");
			for(int i = 0; i < NodeColliders.Length; i++){
				NodeColliders[i].GetComponent<BoxCollider>().enabled = false;
			}

			IsHittenNotExactConnection = Physics.Raycast(GameObjectHiten.transform.position, Point - GameObjectHiten.transform.position, out NotExactCennectionHit);
		}

		else{
			if(NodeColliders != null){
				for(int i = 0; i < NodeColliders.Length; i++){
					NodeColliders[i].GetComponent<BoxCollider>().enabled = true;
				}
			}
		}

		//Not Exact Connection
		if (IsHittenNotExactConnection){

			LineRendererComponent.SetPosition(1, NotExactCennectionHit.transform.position);
			if(Input.GetMouseButtonDown(0)){
				MadeConnection = true;

				//Set Line
				GameObject Line = Instantiate(LineRendererGameObject, LineRendererGameObject.transform.position, Quaternion.identity) as GameObject;
				LineRenderer LineRendererNew = Line.GetComponent<LineRenderer>();
				LineRendererNew.SetColors(Color.white, Color.white);
				LineRendererNew.SetPosition(0, GameObjectHiten.transform.position);
				LineRendererNew.SetPosition(1, NotExactCennectionHit.transform.position);
				LineRendererNew.enabled = true;

				//TODO: Attach Collider to delete Lines

			}
		}

	}

}