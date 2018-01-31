using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class LevelMessionData{
    public int missionId;
    public int count;
    public string data;

    public static LevelMessionData deserialize(DataStream datastream)
    {
        LevelMessionData data = new LevelMessionData();
        data.missionId = datastream.ReadSInt32();
        data.count = datastream.ReadSInt32();
        data.data = datastream.ReadString16();
        return data;
    }

    public void serialize(DataStream datastream)
    {
        datastream.WriteSInt32(missionId);
        datastream.WriteSInt32(count);
        datastream.WriteString16(data);
    }

}
[Serializable]
public class MSLevelOptionDataModel {
	public float version = 1;
	public string levelName;
	public int mapWidth;
	public int mapHeight;
    public List<LevelMessionData> missionList = new List<LevelMessionData>();

    public void deserialize(DataStream datastream)
    {
        version = (float)datastream.ReadSInt32() / 10000f;
        levelName = datastream.ReadString16();
        mapWidth = datastream.ReadSInt32();
        mapHeight = datastream.ReadSInt32();

        int count = datastream.ReadSInt32();
        for (int i = 0; i < count; i ++){
            missionList.Add(LevelMessionData.deserialize(datastream));
        }
    }

    public void serialize(DataStream datastream)
    {
        datastream.WriteSInt32((int)(version * 10000));
        datastream.WriteString16(levelName);
        datastream.WriteSInt32(mapWidth);
        datastream.WriteSInt32(mapHeight);

        int count = missionList.Count;
        datastream.WriteSInt32(count);
        Debug.Log("serialize : " + count);
        for (int i = 0; i < count; i++)
        {
            missionList[i].serialize(datastream);
        }
    }


    public void SetLevelOption(LevelOption lo)
    {
        lo.version = version;
        lo.levelName = levelName;
        lo.mapWidth = mapWidth;
        lo.mapHeight = mapHeight;
        lo.missionList = missionList;
    }

    public void SetValByLevelOption(LevelOption lo)
    {
        version = Tools.GAME_DATA_VERSION;
        levelName = lo.levelName;
        mapWidth = lo.mapWidth;
        mapHeight = lo.mapHeight;
        missionList = lo.missionList;
    }
}
