using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;

using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;


public static class Tools {

    public static float GAME_DATA_VERSION = 0.0f;//

    public static string GetLevelDataFilePath(string filename)
    {
        return Application.dataPath + "/Resources/LevelData/" + filename;
    }


    /// <summary>  
    /// 把对象序列化为字节数组  
    /// </summary>  
    public static byte[] SerializeObject(object obj)
    {
        if (obj == null)
            return null;
        //内存实例
        MemoryStream ms = new MemoryStream();
        //创建序列化的实例
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);//序列化对象，写入ms流中  
        byte[] bytes = ms.GetBuffer();
        return bytes;
    }

    /// <summary>  
    /// 把字节数组反序列化成对象  
    /// UserDataMode userdata = (UserDataMode)GameCommon.DeserializeObject(data);
    /// </summary>  
    public static object DeserializeObject(byte[] bytes)
    {
        object obj = null;
        if (bytes == null)
            return obj;
        //利用传来的byte[]创建一个内存流
        MemoryStream ms = new MemoryStream(bytes);
        ms.Position = 0;
        BinaryFormatter formatter = new BinaryFormatter();
        obj = formatter.Deserialize(ms);//把内存流反序列成对象  
        ms.Close();
        return obj;
    }
    /// <summary>
    /// 把字典序列化
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static byte[] SerializeDic<T>(Dictionary<string, T> dic)
    {
        if (dic.Count == 0)
            return null;
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, dic);//把字典序列化成流
        byte[] bytes = ms.GetBuffer();

        return bytes;
    }
    /// <summary>
    /// 反序列化返回字典
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static Dictionary<string, T> DeserializeDic<T>(byte[] bytes)
    {
        Dictionary<string, T> dic = null;
        if (bytes == null)
            return dic;
        //利用传来的byte[]创建一个内存流
        MemoryStream ms = new MemoryStream(bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        //把流中转换为Dictionary
        dic = (Dictionary<string, T>)formatter.Deserialize(ms);
        return dic;
    }

    /// <summary>
    /// 把字List序列化
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static byte[] SerializeList<T>(List<T> dic)
    {
        if (dic.Count == 0)
            return null;
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, dic);//把字典序列化成流
        byte[] bytes = ms.GetBuffer();

        return bytes;
    }
    /// <summary>
    /// 反序列化返回List
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static List<T> DeserializeList<T>(byte[] bytes)
    {
        List<T> list = null;
        if (bytes == null)
            return list;
        //利用传来的byte[]创建一个内存流
        MemoryStream ms = new MemoryStream(bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        //把流中转换为List
        list = (List<T>)formatter.Deserialize(ms);
        return list;
    }

    //base64字符串解码
    public static string UnBase64String(string value)
    {
        if (value == null || value == "")
        {
            return "";
        }
        byte[] bytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(bytes);
    }

    //base64字符串编码
    public static string ToBase64String(string value)
    {
        if (value == null || value == "")
        {
            return "";
        }
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 二进制数据写入文件 Writes the byte to file.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="tablename">path.</param>
    public static void WriteByteToFile(byte[] data, string path)
    {

        FileStream fs = new FileStream(path, FileMode.Create);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    /// <summary>
    /// 读取文件二进制数据 Reads the byte to file.
    /// </summary>
    /// <returns>The byte to file.</returns>
    /// <param name="path">Path.</param>
    public static byte[] ReadByteToFile(string path)
    {
        //如果文件不存在，就提示错误
        if (!File.Exists(path))
        {
            Debug.Log("读取失败！不存在此文件");
            return null;
        }
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        byte[] data = br.ReadBytes((int)br.BaseStream.Length);

        fs.Close();
        return data;
    }
    /// <summary>
    /// 解压  Uns the G zip.
    /// </summary>
    /// <returns>The G zip.</returns>
    /// <param name="byteArray">Byte array.</param>
    public static byte[] UnGZip(byte[] byteArray) 
    { 
        GZipInputStream gzi = new GZipInputStream(new MemoryStream(byteArray));
        
        //包数据解大小上限为50000
        MemoryStream re = new MemoryStream(50000);
        int count;
        byte[] data = new byte[50000];
        while ((count = gzi.Read(data, 0, data.Length)) != 0)
        {
            re.Write(data, 0, count);
        }
        byte[] overarr = re.ToArray();
        return overarr; 
    } 
    /// <summary>
    /// 
    /// 压缩   Compresses the G zip.
    /// </summary>
    /// <returns>The G zip.</returns>
    /// <param name="rawData">Raw data.</param>
    public static byte[] CompressGZip(byte[] rawData)
    {
        MemoryStream ms = new MemoryStream();
        GZipOutputStream compressedzipStream = new GZipOutputStream(ms);
        compressedzipStream.Write(rawData, 0, rawData.Length);
        compressedzipStream.Close();
        return ms.ToArray();
    }
}
