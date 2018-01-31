using UnityEngine;
using System.Collections;

using UnityEditor;
using System.IO;
using System.Collections.Generic;

using LitJson;

public class MELoadLevelData : EditorWindow {

	public static List<string> fileDataList = new List<string>();
	
	public Vector2 scrollPosition = Vector2.zero;

	void Awake()
	{
		title = "载入关卡";
	}

	[MenuItem("关卡编辑器/载入关卡")]
	public static void OpenLoadLevel(){
		fileDataList.Clear();

		string[] files = Directory.GetFiles(Tools.GetLevelDataFilePath(""));
		for(int i = 0 ; i < files.Length ; i++){
			string[] fileFolders = files[i].Split('/');

			string[] filenames = fileFolders[fileFolders.Length - 1].Split('.');

			if(filenames.Length == 2){
				if(filenames[1] == "lmd"){
					fileDataList.Add(filenames[0]);
				}
			}

		}
		Open();
	}

	[MenuItem("关卡编辑器/新关卡")]
	public static void NewLevel(){
		MSLevelOptionDataModel od = new MSLevelOptionDataModel();
        od.version = Tools.GAME_DATA_VERSION;
		od.levelName = "newlevel";
		od.mapWidth = 100;
		od.mapHeight = 100;


		List<GameObject> delarr = new List<GameObject>();
		bool isfind = false;
		foreach (GameObject sceneObject in Object.FindObjectsOfType(typeof(GameObject))){
			if( sceneObject.name == "LevelOption"){
				LevelOption me = sceneObject.GetComponent<LevelOption>();
                od.SetLevelOption(me);
                Tools.GAME_DATA_VERSION = od.version;

				isfind = true;
			}else if(sceneObject.name != "Main Camera"){
				delarr.Add(sceneObject);

			}
		}
		foreach(GameObject obj in delarr){
			DestroyImmediate(obj);
		}
		if(!isfind){
			GameObject tGO = new GameObject("LevelOption");  
            LevelOption lo = tGO.AddComponent<LevelOption>();  
            od.SetLevelOption(lo);
            Tools.GAME_DATA_VERSION = od.version;
		}
	}

	public static EditorWindow Open()
	{
		MELoadLevelData window = (MELoadLevelData)GetWindow<MELoadLevelData>();
		window.Show();
		return window;
	}

	public static EditorWindow Closeed(){
		MELoadLevelData window = (MELoadLevelData)GetWindow<MELoadLevelData>();
		window.Close();
		return window;
	}

	void OnGUI()
	{
		scrollPosition = GUILayout.BeginScrollView(
			scrollPosition,
			GUILayout.Width(position.width),
			GUILayout.Height(position.height)
			);
		
		
		for(int i = 0 ;i < fileDataList.Count ; i ++){
			if (GUILayout.Button(fileDataList[i], GUILayout.Width(220), GUILayout.Height(32)))
			{
				SelectLevel(i);
			}
		}
		GUILayout.EndScrollView();
		
	}

	void SelectLevel(int index){
		Debug.Log("sel index : " + index);
		LoadLevel(fileDataList[index]);
	}

	public void ReloadScene(DataStream datastream){

		Debug.Log("ReloadScene");

		MSLevelOptionDataModel od = new MSLevelOptionDataModel();
        od.deserialize(datastream);
		//od.version = (float)datastream.ReadSInt32() / 10000f;
		//od.levelName = datastream.ReadString16();
		//od.mapWidth = datastream.ReadSInt32();
		//od.mapHeight = datastream.ReadSInt32();


		List<GameObject> delarr = new List<GameObject>();
		LevelOption me = null;
		foreach (GameObject sceneObject in Object.FindObjectsOfType(typeof(GameObject))){
			if( sceneObject.name == "LevelOption"){
                me = sceneObject.GetComponent<LevelOption>();
                od.SetLevelOption(me);

                Tools.GAME_DATA_VERSION = od.version;
            }else if(sceneObject.name != "Main Camera" && sceneObject.name != "Directional light"){
				delarr.Add(sceneObject);
			}
		}

		//创建关卡配置物体
		if(me == null){
			GameObject tGO = new GameObject("LevelOption");  
			me = tGO.AddComponent<LevelOption>();  
            od.SetLevelOption(me);
            Tools.GAME_DATA_VERSION = od.version;
		}

		foreach(GameObject obj in delarr){
			DestroyImmediate(obj);
		}
		int objcount = datastream.ReadSInt32();

		Dictionary<int, MSBaseObject> dic = new Dictionary<int, MSBaseObject>();
        Debug.Log("objcount : " + objcount);
		for(int i = 0 ; i < objcount ;i ++){
			//MSBaseObject.CreateObj(datastream);
			//从字节流中获取id
			int type = datastream.ReadSInt32();
            string id = datastream.ReadString16();

			GameObject go;

			if(type == -1){
				//组物体
				go = new GameObject("LevelOption");  
			}else{
				
                Object tempObj = Resources.Load("Prefabs/MapObj/" + id) as GameObject;
                if (tempObj == null) {
					Debug.Log(type + "/" + id + " is null!");
                }
				go = (GameObject)Instantiate(tempObj);
			}

			MSBaseObject baseobj = go.GetComponent<MSBaseObject>();

			if(baseobj == null){
				baseobj = go.AddComponent<MSBaseObject>();
			}
			//baseobj.itemtype = type;
			baseobj.itemid = id;

			baseobj.Deserialize(datastream);
			baseobj.init() ;


			if(dic.ContainsKey(baseobj.parent)){
				go.transform.parent = dic[baseobj.parent].transform;
			}else{
				go.transform.parent = me.transform;
			}

			dic.Add(baseobj.myData.instanceID,baseobj);
		}
	}

	public void LoadLevel(string fileName){
        byte[] levelGzipData = Tools.ReadByteToFile(Tools.GetLevelDataFilePath(fileName+".lmd"));
        byte[] levelData = Tools.UnGZip(levelGzipData);
		DataStream datastream = new DataStream(levelData,true);

		ReloadScene(datastream);
		Closeed();
	}



}
