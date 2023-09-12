[System.Serializable]
public class DataToSave 
{
    public int[] puntosPorNivel = new int[5];
    public int puntosModoLibre;

    public DataToSave(int puntos, int nivel, int[] anteriorespuntos)
    {
        puntosPorNivel = anteriorespuntos;
        nivel--;
        puntosPorNivel[nivel] = puntos;
    }
    
    public DataToSave(int puntos) 
    {
        puntosModoLibre = puntos;

    }

}
