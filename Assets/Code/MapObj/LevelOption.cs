using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelOption : MonoBehaviour {

	public string levelName = "";

	public float version = 0.0f;

	public float width = 1.0f;
	public float height = 1.0f;

	public int mapHeight = 10;
	public int mapWidth = 100;

    public List<LevelMessionData> missionList = new List<LevelMessionData>();

	//public float autoDPos = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

}
