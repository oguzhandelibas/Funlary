using UnityEngine;

namespace Funlary
{
    [CreateAssetMenu(fileName = "NameData", menuName = "ScriptableObjects/NameData", order = 1)]
    public class NameData : ScriptableObject
    {
        public string[] Names = {
        "NeşeliNar", "KıkırKöpük", "GülümseGüneş", "MizahMaratonu", "KeyifKelebeği",
        "ŞakacıŞahin", "KahkahaKervanı", "GeyikGözlü", "CapcanlıCıvılCıvıl", "KıkırKedicik",
        "NeşeNebatı", "ZıpırZambak", "KomikKuşçu", "GülGazeli", "NeşeNinjası",
        "ŞenŞakrak", "KahkahaKuşu", "KeyifliKaktüs", "CoşkuCüce", "NeşeNinja",
        "KıpırKırık", "MuzipMangal", "GırgırGözlü", "ŞenŞehir", "KıkırKarpuz",
        "MizahMeraklısı", "GeyikGözlüGüvercin", "NeşeNur", "KıpırKıtır", "ŞakırŞakrak",
        "NeşeliNalbant", "GülGözlüGöğüs", "GırgırGümbür", "NeşeliNisan", "KıkırKuzey",
        "GülGözlüGöçmen", "ŞakırŞakrak", "NeşeliNazar", "MizahMuz", "GeyikGözlüGöğü",
        "KıpırKüçük", "NeşeliNesve", "KahkahaKedi", "GırgırGümbür", "NeşeNisan",
        "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "GırgırGümbür", "KahkahaKürk",
        "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele", "KahkahaKedi",
        "NeşeNisan", "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet",
        "GeyikGözlüGüneş", "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele",
        "ŞenŞeftali", "GeyikGözlüGüneş", "GırgırGümbür", "KıkırKuzey", "NeşeNisan",
        "KıpırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele", "KahkahaKedi",
        "NeşeNisan", "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet",
        "GeyikGözlüGüneş", "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele",
        "ŞenŞeftali", "GeyikGözlüGüneş", "GırgırGümbür", "KıkırKuzey", "NeşeNisan",
        "KıpırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele", "KahkahaKedi",
        "NeşeNisan", "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet",
        "GeyikGözlüGüneş", "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele",
        "ŞenŞeftali", "GeyikGözlüGüneş", "GırgırGümbür", "KıkırKuzey", "NeşeNisan",
        "KıpırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele", "KahkahaKedi",
        "NeşeNisan", "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet",
        "GeyikGözlüGüneş", "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele",
        "ŞenŞeftali", "GeyikGözlüGüneş", "GırgırGümbür", "KıkırKuzey", "NeşeNisan",
        "KıpırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele", "KahkahaKedi",
        "NeşeNisan", "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet",
        "GeyikGözlüGüneş", "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele",
        "ŞenŞeftali", "GeyikGözlüGüneş", "GırgırGümbür", "KıkırKuzey", "NeşeNisan",
        "KıpırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet", "GeyikGözlüGüneş",
        "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele", "KahkahaKedi",
        "NeşeNisan", "KıkırKuzey", "GülGözlüGuguk", "ÇılgınÇavdar", "MizahMagnet",
        "GeyikGözlüGüneş", "ŞakacıŞakak", "NeşeliNane", "KıpırKabak", "MizahMücadele"
    };
    }
}
