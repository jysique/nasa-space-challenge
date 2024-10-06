using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Exoplanet", menuName = "ExoplanetDex/Exoplanet", order = 0)]

public class Exoplanet : ScriptableObject
{
    public enum SystemPlanet
    {
        Desconocido = 0,
        Trappist_1 = 1,
        HR_8799 = 2,
    }
    public enum Livable
    {
        Desconocido = 0,
        Posible_habitable = 1,
        Habitable = 2,
        No_Habitable = 3,
        Posible_No_Habitable = 4,
    }
    public enum Composition
    {
        Desconocido = 0,
        Gaseoso = 1,
        Oceánico = 2,
        Rocoso =3,
    }
    public string exo_name;
    [TextAreaAttribute]
    public string exo_desc;
    public SystemPlanet sistema;
    public Livable livable;
    [HelpBoxAttribute("Diametro con relacion a la tierra")]
    public float size;
    public Composition[] composition;
    public float max_temp;
    public float min_temp;
    [HelpBoxAttribute("Gravedad con relacion a la tierra")]
    public float gravity;
    public int year_days;

}
