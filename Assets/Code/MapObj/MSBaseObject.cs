using UnityEngine;
using System.Collections;

/**
 * 游戏中全部对象继承自此类
 */
public class MSBaseObject : MonoBehaviour  {
	public MSBaseObjectData myData = new MSBaseObjectData();

	private int itemtype = 0;
	public string itemid="";

	public int parent;

	public static MSBaseObject CreateObj(DataStream datastream){
		//从字节流中获取id
		int type = datastream.ReadSInt32();
        string id = datastream.ReadString16();

		//获取配置表文件
		MSBaseObject baseobj = CreateById(type,id);

		baseobj.Deserialize(datastream);
		baseobj.init() ;

		return baseobj;
	}

	public static MSBaseObject CreateById(int type,string id){
		//获取配置表文件

		GameObject go= Resources.Load("Prefabs/MapObj/" + id) as GameObject;
		GameObject tempObj = (GameObject)Instantiate(go);
		
		MSBaseObject baseobj = tempObj.GetComponent<MSBaseObject>();
		baseobj.itemtype = type;
		baseobj.itemid = id;
		if(baseobj == null){
			baseobj = tempObj.AddComponent<MSBaseObject>();
		}
		return baseobj;
	}


	public void init(){
		name = myData.Name;

		transform.position = myData.Position;
		transform.rotation  = Quaternion.Euler(myData.Rotation);
		transform.localScale = myData.Scale;

	}

	public void SaveData(){
		myData.Position =  ( transform.position );
		myData.Rotation =  ( transform.rotation.eulerAngles );
		myData.Scale =  ( transform.localScale );
	}

	public virtual void Serialize(DataStream writer)
	{
		writer.WriteSInt32(itemtype);
        writer.WriteString16(itemid);
        Debug.Log("itemid : " + itemid);
		myData.Serialize(writer);
		writer.WriteSInt32(parent);

	}
	
	public virtual void Deserialize(DataStream reader)
	{
		myData.Deserialize(reader);
		parent = reader.ReadSInt32();
	}
}
