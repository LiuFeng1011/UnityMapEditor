using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class V3{
	public float x;
	public float y;
	public float z;

	public V3(){
	}

	public V3(float x,float y,float z){
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public void InitByVector3(Vector3 v){
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
	}

	public Vector3 GetVector3(){
		Vector3 ret = new Vector3();
		ret.x = x;
		ret.y = y;
		ret.z = z;
		return ret;
	}
}

[Serializable]
public class MSBaseObjectData {
	private const int floatRetainPlace = 1000;
	private string name = "";

	public string Name{
		get{return name;}
		set{name = value;}
	}

	public int objID ;

	public int instanceID;

	private V3 position = new V3();
	public Vector3 Position{
		get{return position.GetVector3();}
		set{position.InitByVector3(value);}
	}

	private V3 rotation = new V3();
	public Vector3 Rotation{
		get{return rotation.GetVector3();}
		set{rotation.InitByVector3(value);}
	}
	private V3 scale = new V3();
	public Vector3 Scale{
		get{return scale.GetVector3();}
		set{scale.InitByVector3(value);}
	}

	public virtual void Serialize(DataStream writer)
	{

		writer.WriteString16(name);
		writer.WriteSInt32((int)objID);
		writer.WriteSInt32((int)instanceID);

		writer.WriteSInt32((int)(Position.x * floatRetainPlace));
		writer.WriteSInt32((int)(Position.y * floatRetainPlace));
		writer.WriteSInt32((int)(Position.z * floatRetainPlace));
		writer.WriteSInt32((int)(Rotation.x * floatRetainPlace));
		writer.WriteSInt32((int)(Rotation.y * floatRetainPlace));
		writer.WriteSInt32((int)(Rotation.z * floatRetainPlace));
		writer.WriteSInt32((int)(Scale.x * floatRetainPlace));
		writer.WriteSInt32((int)(Scale.y * floatRetainPlace));
		writer.WriteSInt32((int)(Scale.z * floatRetainPlace));

	}
	
	public virtual void Deserialize(DataStream reader)
	{
		name = reader.ReadString16();
		objID = reader.ReadSInt32();
		instanceID = reader.ReadSInt32();

		Position = new Vector3((float)reader.ReadSInt32() / floatRetainPlace,
		                       (float)reader.ReadSInt32() / floatRetainPlace,
		                       (float)reader.ReadSInt32() / floatRetainPlace);

		Rotation = new Vector3((float)reader.ReadSInt32() / floatRetainPlace,
		                       (float)reader.ReadSInt32() / floatRetainPlace,
		                       (float)reader.ReadSInt32() / floatRetainPlace);
		
		Scale = new Vector3((float)reader.ReadSInt32() / floatRetainPlace,
		                    (float)reader.ReadSInt32() / floatRetainPlace,
		                    (float)reader.ReadSInt32() / floatRetainPlace);

	}

}
