using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(LevelOption))]
public class LevelOptionInspector : Editor {
    LevelOption lo;

    void OnEnable()
    {
        //获取当前编辑自定义Inspector的对象
        lo = (LevelOption)target;
    }


    public override void OnInspectorGUI()
    {
        //设置整个界面是以垂直方向来布局
        EditorGUILayout.BeginVertical();

        //空两行
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    //    public string levelName = "";

    //public float version = 0.0f;

    //public float width = 1.0f;
    //public float height = 1.0f;

    //public int mapHeight = 10;
    //public int mapWidth = 100;
        lo.levelName = EditorGUILayout.TextField("关卡名称", lo.levelName);
        lo.version = EditorGUILayout.FloatField("版本号", lo.version);

        //任务列表
        int count = EditorGUILayout.IntField("任务数量", lo.missionList.Count);


    }
}
