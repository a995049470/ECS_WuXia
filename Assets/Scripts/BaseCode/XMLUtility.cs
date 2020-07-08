using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XMLUtility 
{
    public static string ToXML<T>(T obj)
    {
        string str = "";
        StringWriter sw = new StringWriter();
        XmlSerializer sz = new XmlSerializer(typeof(T));
        sz.Serialize(sw, obj);
        str = sw.ToString();
        sw.Close();
        return str;
    }

    public static T FromXML<T>(string str)
    {
        T value = default(T);
        if(string.IsNullOrEmpty(str))
        {
            return value;
        }
        StringReader sr = new StringReader(str);
        XmlSerializer sz = new XmlSerializer(typeof(T));
        value = (T)sz.Deserialize(sr);
        sr.Close();
        return value;
    }

    public static void Save<T>(string key, T obj)
    {
        string xmlStr = ToXML(obj);
        PlayerPrefs.SetString(key, xmlStr);
    }

    public static T Load<T>(string key)
    {
        T value = default(T);
        if(!PlayerPrefs.HasKey(key))
        {
            return value;
        }
        value = FromXML<T>(PlayerPrefs.GetString(key));
        return value;
    }

}
