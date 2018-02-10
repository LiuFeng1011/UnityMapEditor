using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AHIntTest();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AHIntTest(){
        AHInt test = 1;
        test += 2;
        int x = 3;
        test += x;
        Debug.Log("[AHIntTest] : " + test);
    }


    public static void Base64Test(){
        string base64string = Tools.ToBase64String("aaaa11233Base64编码和解码");
        
        string unbase64string = Tools.UnBase64String(base64string);
        
        Debug.Log("base64string : " + base64string);
        Debug.Log("unbase64string : " + unbase64string);
    }
    public static void GZipTest()
    {
        string testdata = "aaaa11233GZip压缩和解压";

        byte[] gzipdata = Tools.CompressGZip(Encoding.UTF8.GetBytes(testdata));
        byte[] undata = Tools.UnGZip(gzipdata);

        Debug.Log("[GZipTest]  : data" + Encoding.UTF8.GetString(undata));
    }


    public static void SerializeDicTest(){
        
        Dictionary<string, int> test = new Dictionary<string, int>();
        
        test.Add("1",1);
        test.Add("2",2);
        test.Add("3",4);
        
        byte[] testbyte = Tools.SerializeObject(test);
        
        Dictionary<string, int> testdic = (Dictionary<string, int>)Tools.DeserializeObject(testbyte);
        
        Debug.Log("[SerializeDicTest]  : " + testdic["3"]);
    }

}
