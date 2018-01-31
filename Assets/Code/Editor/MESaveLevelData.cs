using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class MESaveLevelData : Editor
{
	public static List<MSBaseObject> msobjlist = new List<MSBaseObject>();

	[MenuItem("关卡编辑器/保存关卡")]
	public static void SaveLevel(){
		Debug.Log("SaveLevel");
		msobjlist.Clear();
		// 场景路径
		/*
		string scenePath = AssetDatabase.GetAssetPath(selectObject);
		
		Debug.Log("=====================================");
		Debug.Log(sceneName + "   path : " + scenePath );
		// 打开这个关卡
		EditorApplication.OpenScene(scenePath);
		*/
		
		MSLevelOptionDataModel od = null;
		DataStream stream = new DataStream(true);

		//获取场景中全部道具
		Object[] objects = Object.FindObjectsOfType(typeof(GameObject));

		foreach (GameObject sceneObject in objects){
			if(sceneObject.name == "LevelOption"){
				LevelOption editor = sceneObject.GetComponent<LevelOption>();
				if(editor != null){
					od = new MSLevelOptionDataModel();
                    od.SetValByLevelOption(editor);

                    od.serialize(stream);

                    Tools.GAME_DATA_VERSION = od.version;
				}

				ForeachObjAndSave(sceneObject);
			}
		}

		Debug.Log("save list count:"+msobjlist.Count);
		stream.WriteSInt32(msobjlist.Count);

		foreach(MSBaseObject msobj in msobjlist){
			msobj.Serialize(stream);
		}

		if(od == null){
			Debug.LogError("can't find the level option!");
			return ;
		}

		WriteDataToFile(od,stream);
	}

	static void ForeachObjAndSave(GameObject obj){
		foreach (Transform child in obj.transform)
		{
			MSBaseObject msobj = child.GetComponent<MSBaseObject>();

			if(msobj == null){
				//if(child.childCount <= 0) continue;
				//msobj = child.gameObject.AddComponent<MSBaseObject>();
				//msobj.myData.objID = -1;
                continue;
			}

			msobj.myData.Name = child.name;
			msobj.SaveData();
			msobj.myData.instanceID = child.GetInstanceID();
			msobj.parent = child.parent.GetInstanceID();
            Debug.Log("obj : " + msobj.itemid);
			msobjlist.Add(msobj);

			ForeachObjAndSave(child.gameObject);

		}
	}

	static void WriteDataToFile(MSLevelOptionDataModel od ,DataStream data){
        string filePath = Tools.GetLevelDataFilePath(od.levelName + ".lmd");

		byte[] objData = data.ToByteArray();
        byte[] objGzipData = Tools.CompressGZip(objData);
        Tools.WriteByteToFile(objGzipData,filePath );

		Debug.Log("save path : " + filePath);
		EditorUtility.DisplayDialog("导出关卡" + od.levelName + "成功！", filePath,  "确定");
	}

}
