using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public  static class SaveManager 
{

    public static int[] puntosNivel = new int[5];

    //public static void GuardarPuntosNivel(int nivel, int puntos)
    //{
    //    nivel--;
    //    puntosNivel[nivel] = puntos;
    //}


    public static void Guardar (int puntos, int nivel)
    {

        DataToSave data = new DataToSave(puntos, nivel, puntosNivel);

        string dataPath = Application.persistentDataPath + "/score.txt";

        FileStream fileStream = new FileStream(dataPath, FileMode.Create); 
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void Guardar2(int puntos)
    {

        DataToSave data = new DataToSave(puntos);

        string dataPath = Application.persistentDataPath + "/score.txt";

        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }
    //public static DataToSave Cargar()
    //{
    //    string dataPath = Application.persistentDataPath + "/score.txt";

    //    if(File.Exists(dataPath))
    //    {
    //        FileStream fileStream = new FileStream(dataPath, FileMode.Open);
    //        BinaryFormatter binaryFormatter = new BinaryFormatter();
    //        DataToSave data=(DataToSave) binaryFormatter.Deserialize(fileStream);   
    //        fileStream.Close ();
    //        asociar(data);
    //        return data;
    //    }
    //    else
    //    {
    //        return null;
    //    }

    //}


    public static DataToSave Cargar()
    {
        string dataPath = Application.persistentDataPath + "/score.txt";

        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            DataToSave data = (DataToSave)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            asociar(data);
            return data;
        }
        else
        {
            return null;
        }

    }


    private static void asociar(DataToSave data) 
    {
        for(int i = 0; i<data.puntosPorNivel.Length; i++) 
        {
            if (data.puntosPorNivel[i] > 0)
            {
                Debug.Log("Pts: " + data.puntosPorNivel[i]);
                puntosNivel[i] = data.puntosPorNivel[i];
            }
        }
        


    }

}
