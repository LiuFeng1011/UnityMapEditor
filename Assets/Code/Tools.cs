using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;

public static class Tools {

    public static float GAME_DATA_VERSION = 0.0f;//

    public static string GetLevelDataFilePath(string filename)
    {
        return Application.dataPath + "/Resources/LevelData/" + filename;
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
