using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Invatare
{
    public class Car
    {
        //Proprietati/Fields

        //Cu un get/set ne putem incapsula proprietatea noastra ( in acest caz IdCar, Marca, etc.) pentru securitate

        //Get ma ajuta sa obtin valoarea in variabila Marca din variabila aMarca (din constructor. Se mai spune Atribut)
        //Set ma ajuta sa pot modifica valoarea. De asemenea, se pot pune diferite reguli. Vezi exemplul la AnFabricatie
        public int IdCar { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }

        //Metoda 1 - unde putem pune si reguli pentru setarea valorii


        public int AnFabricatie { get; set; }
        public Car(int aIdCar, string aMarca, string aModel, int aAnFabricatie)
        {
            IdCar = aIdCar;
            Marca = aMarca;
            Model = aModel;
            AnFabricatie = aAnFabricatie;

        }
            public bool EsteNoua()
        {
            if (AnFabricatie > 2006)
            {
                return true;
            }
            else
            {
                return false;
            }
               
         }
            
    }
        /*   public Car()      Metoda 1:  Acest constructor este ByDefault si se creaza el automat daca nu il cream noi
           {
           }  */


        // In interior sunt argumente pe care le Pasam catre Proprietati/Fields.
        // Prin constructor ii spunem cum dorim sa arate obiectul nostru

}
