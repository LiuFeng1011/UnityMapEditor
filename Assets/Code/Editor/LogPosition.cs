using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LogPosition : EditorWindow {

    //最终输出的数据
	static string logtext;

    //增加菜单栏选项
	[MenuItem("LOGPOSITION/LOG")]
	public static void OpenLoadLevel(){
        //重置数据
		logtext = "";
        //获取编辑器中当前选中的物体
		GameObject obj = Selection.activeGameObject;

        //如果没有选择任何物体，弹出提示并退出
        if(obj == null){
            EditorUtility.DisplayDialog("ERROR", "No select obj!!", "ENTRY");
            return;
        }

        //遍历所有子物体，并记录数据
		ForeachObjAndSave(obj);
		Debug.Log(logtext);

		//复制到剪贴板  
		TextEditor editor = new TextEditor();  
		editor.content = new GUIContent(logtext);  
		editor.SelectAll();  
		editor.Copy();  
	}

    //遍历所有子物体
	static void ForeachObjAndSave(GameObject obj){
		foreach (Transform child in obj.transform)
		{
			logtext+= (child.localPosition.x + "," + child.localPosition.y + "," + child.localPosition.z + "\n");
		}
	}
}
