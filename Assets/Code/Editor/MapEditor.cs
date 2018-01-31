using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class MapEditor : MonoBehaviour {

	public string levelName = "";

	public float version = 0.0f;

	public float width = 10.0f;
	public float height = 1.0f;

    public int baseHeight = 6;

	public int mapHeight = 10;
	public int mapWidth = 100;

	public bool isShowBGLine = true;

	public bool isAutoPos = false;
	//public float autoDPos = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		AutoSetPos();

	}


	void OnDrawGizmos()
	{
		
		if(isShowBGLine ){
			DrawBGLine();
		}

	}

	public void DrawBGLine(){
		Gizmos.color = new Color(0f,0f,0f,1f);
		
        Gizmos.DrawLine(new Vector3(0, baseHeight , -1f),new Vector3(mapWidth, baseHeight , -1f));
        Gizmos.DrawLine(new Vector3(mapWidth, baseHeight , -1f),new Vector3(mapWidth, mapHeight +baseHeight, -1f));
        Gizmos.DrawLine(new Vector3(mapWidth, mapHeight +baseHeight, -1f),new Vector3(0, mapHeight+baseHeight , -1f));
        Gizmos.DrawLine(new Vector3(0, mapHeight + baseHeight , -1f),new Vector3(0, baseHeight , -1f));
		
		
		Gizmos.color = new Color(1f,1f,0f,0.2f);
		
		
        for (float y = baseHeight; y < mapHeight+baseHeight; y += height)
		{
			Gizmos.DrawLine(new Vector3(0, y , -1f),
			                new Vector3(mapWidth, y , -1f));
			
            Handles.Label(new Vector3(-1f,y+0.5f , 0f), "" + y);
		}
		
		for (float x = 0; x < mapWidth; x += width)
		{
            Gizmos.DrawLine(new Vector3(x,baseHeight, -1f),
                            new Vector3(x,mapHeight+baseHeight, -1f));
			
            Handles.Label(new Vector3(x,baseHeight-0.2f, 0f), "" + x);
		}
	}

	public void AutoSetPos(){
		if(!isAutoPos ){
			return;
		}

		//foreach (GameObject sceneObject in Object.FindObjectsOfType(typeof(GameObject))){

		foreach (GameObject sceneObject in  Selection.gameObjects){
			SetObjPos(sceneObject,sceneObject.transform.position);
		}
	}

	public int GetSymbol(float val,float target){
		if(val > 0 && target < 0){
			return -1;
		}else if(val < 0 && target > 0){
			return -1;
		}
		return 1;
	}

	public static void SetObjPos(GameObject obj,Vector3 pos){

		MapEditor me = GameObject.Find("levelOption").transform.GetComponent<MapEditor>();

		if(me == null){
			EditorUtility.DisplayDialog("ERROR", "Cant't find the 'levelOption' !!",  "ENTRY");
			return;
		}

		float autoDPosx = me.width;
		float autoDPosy = me.height;

		float alignedx = 0;
		float alignedy = 0;

		if(autoDPosx == 0){
			alignedx = pos.x;
		}else{
			alignedx = Mathf.Floor(pos.x/autoDPosx)*autoDPosx + autoDPosx/2.0f;
		}
		if(autoDPosy == 0){
			alignedy = pos.y;
		}else{
			alignedy = Mathf.Floor(pos.y/autoDPosy)*autoDPosy + autoDPosy/2.0f;
		}
		Vector3 aligned = new Vector3(alignedx,alignedy, 0.0f);
		obj.transform.position = aligned;
	}
}
