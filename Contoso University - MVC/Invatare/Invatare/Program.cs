using Invatare;

/* Daca constructorul(blueprint-ul clasei noastre) nu avea deloc argumente, trebuie sa scriem manual fiecare proprietate dupa instantierea clasei 
Car Car = new Car();
Car.IdCar = 1;
Car.Marca = "Subaru";                   Metoda 1
Car.Model = "Legacy";
Car.AnFabricatie = 2006; */

Car Car1 = new Car(1, "Subaru", "Legacy", 2006); // Metoda 2 - Ne folosim de numele obiectului creat pentru a face ce avem nevoie + Scriere mai rapida/usoara
Car Car2 = new Car(2, "Mitsubishi", "Lancer", 2017);
Console.WriteLine("Cum sa folosim un Constructor");
Console.WriteLine("                  ");

void Salutare()  //Metoda Creata
{
    Console.WriteLine(Car1.IdCar + "  " + Car1.Marca + "  " + Car1.Model + "  " + Car1.AnFabricatie);
    Console.WriteLine(Car1.IdCar + "  " + Car1.Marca + "  " + Car1.Model + "  " + Car1.AnFabricatie);
    Console.WriteLine("                                          ");
}
//Apelare Metoda
Console.WriteLine(Car1.EsteNoua());
Console.WriteLine("                  ");
Console.WriteLine("                  ");
Console.WriteLine("Usage of Generics");
Console.WriteLine("                  ");

int[] intArray = { 1, 2, 3, 4, 5 };
double[] doubleArray = { 1.3, 2.4, 3.6, 4.9 };
String[] stringArray = { "Mama", "Tata", "Bunicu", "Bunica" };


// In loc sa folosim 3 metode (exemplul actual) pentru a afisa elementele fiecarui tip de Array
// putem folosi un generic de care ne putem folosi indiferent de DataType (int,float,double, int[], etc.)
// Ne ajuta sa putem reutiliza codul/metoda indiferent de tipul de data necesar

static void FolosimGenerice<T>(T[] Array)  // In loc de int[] Array sau double[] Array sau String[] Array, am folosit un "T" (generic) care se aplica pentru toate tipurile de date
{
    foreach (T element in Array)
    {
        Console.Write(element + "  ");
    }
    Console.WriteLine();
}
FolosimGenerice(intArray);
FolosimGenerice(doubleArray);
FolosimGenerice(stringArray);

Console.WriteLine("                  ");
Console.WriteLine("                  ");
Console.WriteLine("       Interfete        ");
Console.WriteLine("                  ");


// Interfete

Catel Labrador = new Catel();
Pisica British = new Pisica();

British.MetodaMiau();
Labrador.MetodaHam();
public interface IMiau
    {
    void MetodaMiau();
    }
public interface IHam
    {
    void MetodaHam();
    }
public class Pisica : IMiau
{   
    public void MetodaMiau()
    {
        Console.WriteLine("Pisica face Miau");
    }
}
public class Catel
{
    public void MetodaHam()
    {
        Console.WriteLine("Catelul face Ham");
    }

}