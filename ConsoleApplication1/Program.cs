using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZOO
{
    class Program
    {
        private static double entropia_atrybutu_decyzyjnego;

        private static double entropia_atrybutu_decyzyjnego_funckcja(List<string[]> wszystkieObiekty1, int wiersze1)
        {
            double entropia_atrybutu_decyzyjnego_1 = 0;
            Dictionary<string, int>[] slownik_wartosci_atrybutow = new Dictionary<string, int>[wszystkieObiekty1[0].Length];
            for (int i = 0; i <= wszystkieObiekty1[0].Length - 1; i++)
            {
                Dictionary<string, int> temp = new Dictionary<string, int>();
                foreach (string[] obiekt in wszystkieObiekty1)
                {
                    if (temp.ContainsKey(obiekt[i]))
                    {
                        temp[obiekt[i]] += 1;
                    }
                    else
                    {
                        temp.Add(obiekt[i], 1);
                    }
                }
                slownik_wartosci_atrybutow[i] = temp;
            }

            //Wyliczenie entropii atrybutu decyzyjnego:


            foreach (KeyValuePair<string, int> element_atrybutu in slownik_wartosci_atrybutow[slownik_wartosci_atrybutow.Length - 1])
            {
                double temp;
                temp = (Convert.ToDouble(element_atrybutu.Value) / wiersze1) * Math.Log(Convert.ToDouble(element_atrybutu.Value) / wiersze1, 2);
                entropia_atrybutu_decyzyjnego_1 += temp;

            }
            entropia_atrybutu_decyzyjnego_1 *= -1;
            return entropia_atrybutu_decyzyjnego_1;

        }

        private static void funkcja(List<string[]> wszystkieObiekty)
        {
           
        }



        static void Main(string[] args)
        {

            //Odczyt pliku tekstowego
            string line;
            System.IO.StreamReader file = new StreamReader("zoo.data");

            //Utworzenie listy wszystkich obiektów
            List<string[]> wszystkieObiekty = new List<string[]>();                 //lista tablic obiektów
            while ((line = file.ReadLine()) != null)
            {
                wszystkieObiekty.Add(line.Split(','));
            }
            file.Dispose();

             int wiersze = wszystkieObiekty.Count();
            int ilosc_wszystkich_obiektow = wszystkieObiekty.Count;

            //Utworzenie słownika wartości wszystkich atrybutów potrzebny do policzenia entropii rozkładu prawdopodobieństwa

            Dictionary<string, int>[] slownik_wartosci_atrybutow = new Dictionary<string, int>[wszystkieObiekty[0].Length];
            for (int i = 0; i <= wszystkieObiekty[0].Length - 1; i++)
            {
                Dictionary<string, int> temp = new Dictionary<string, int>();
                foreach (string[] obiekt in wszystkieObiekty)
                {
                    if (temp.ContainsKey(obiekt[i]))
                    {
                        temp[obiekt[i]] += 1;
                    }
                    else
                    {
                        temp.Add(obiekt[i], 1);
                    }
                }
                slownik_wartosci_atrybutow[i] = temp;
            }

            //Wyliczenie entropii atrybutu decyzyjnego:


            foreach (KeyValuePair<string, int> element_atrybutu in slownik_wartosci_atrybutow[slownik_wartosci_atrybutow.Length - 1])
            {
                double temp;
                temp = (Convert.ToDouble(element_atrybutu.Value) / wiersze) * Math.Log(Convert.ToDouble(element_atrybutu.Value) / wiersze, 2);
                entropia_atrybutu_decyzyjnego += temp;

            }
            entropia_atrybutu_decyzyjnego = -entropia_atrybutu_decyzyjnego;
            //Console.WriteLine("Wartość entropii atrybutu decyzyjnego: " + entropia_atrybutu_decyzyjnego);


            //foreach (string[] i in wszystkieObiekty)
            //{
            //    //przechodzimy po każdej wartości w wierszu
            //    foreach (string j in i)
            //        //wypisujemy każdą wartość, a po niej tabulację
            //        if (j == "overcast")
            //        {
            //            Console.Write("{0} \t|", j);
            //        }
            //        else
            //            Console.Write("{0}\t\t|", j);
            //    //po wypisaniu całego wiersza przechodzimy do nowej linijki
            //    Console.WriteLine();
            //}


            Dictionary<string, int> slownik_atrybuty_kolumny = new Dictionary<string, int>();
            List<Dictionary<string, int>> slownik_atrybuty_kolumny_lista = new List<Dictionary<string, int>>();


            Console.WriteLine(wszystkieObiekty[0].Length);
            for (int a = 0; a < wszystkieObiekty[0].Length - 1; a++)
            {
                Dictionary<string, int> temp = new Dictionary<string, int>();
                foreach (string[] i in wszystkieObiekty)
                {

                    //Console.WriteLine(i[a] + "_" + i[i.Length - 1]);
                    if (temp.ContainsKey(i[a] + "_" + i[i.Length - 1]))
                    {
                        temp[i[a] + "_" + i[i.Length - 1]] += 1;
                    }
                    else
                    {
                        temp.Add(i[a] + "_" + i[i.Length - 1], 1);
                    }

                }
                slownik_atrybuty_kolumny_lista.Add(temp);
            }

            //OBLICZANIE ENTROPII WSZYSTKICH ATRYBUTÓW(kolumn):

            List<Dictionary<string, double>> slownik_entropia_kolumny_lista = new List<Dictionary<string, double>>();
            for (int i = 0; i < slownik_wartosci_atrybutow.Length - 1; i++)
            {
                Dictionary<string, double> temp = new Dictionary<string, double>();
                foreach (KeyValuePair<string, int> wartosc_atrybutu in slownik_wartosci_atrybutow[i])
                {
                    //Console.WriteLine("Nr atrybutu: " + i + ", [wartosc, liczba wystapien]: " + wartosc_atrybutu);
                    foreach (KeyValuePair<string, int> zliczenia_atrybutu in slownik_atrybuty_kolumny_lista[i])
                    {
                        if (zliczenia_atrybutu.Key.Contains(wartosc_atrybutu.Key))
                        {
                            if (temp.ContainsKey(wartosc_atrybutu.Key))
                            {
                                temp[wartosc_atrybutu.Key] += Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value * Math.Log(Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value, 2);
                                Console.WriteLine(Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value * Math.Log(Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value, 2));
                                //Console.WriteLine(test);
                            }
                            else
                            {
                                temp.Add(wartosc_atrybutu.Key, Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value * Math.Log(Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value, 2));
                                Console.WriteLine(Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value * Math.Log(Convert.ToDouble(zliczenia_atrybutu.Value) / wartosc_atrybutu.Value, 2));
                                //Console.WriteLine(test);
                            }
                        }
                    }
                    temp[wartosc_atrybutu.Key] *= Convert.ToDouble(wartosc_atrybutu.Value) / wiersze;
                }

                slownik_entropia_kolumny_lista.Add(temp);
            }


            Dictionary<string, double> slownik_entropia_kolumny = new Dictionary<string, double>();
            int licznik = 0;
            for (int i = 0; i < slownik_wartosci_atrybutow.Length - 1; i++)
            {
                double temp = 0;
                foreach (KeyValuePair<string, double> entropia_kolumny_temp in slownik_entropia_kolumny_lista[i])
                {
                    temp += entropia_kolumny_temp.Value;
                }
                slownik_entropia_kolumny.Add(licznik.ToString(), -temp);
                licznik++;
            }

            int max;
            double max1;
            Dictionary<string, double> slownik_gain = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> entropia_atrybutu in slownik_entropia_kolumny)
            {
                slownik_gain.Add(entropia_atrybutu.Key, entropia_atrybutu_decyzyjnego - entropia_atrybutu.Value);
            }


            max = 0;
            int licznik_gain = 0;
            max1 = slownik_gain.First().Value;

            foreach (KeyValuePair<string, double> gain_atrybutu in slownik_gain)
            {
                if (gain_atrybutu.Value > max1)
                {
                    max1 = gain_atrybutu.Value;
                    max = licznik_gain;
                }
                licznik_gain++;
            }

            Dictionary<string, List<string[]>> lista_lista = new Dictionary<string, List<string[]>>();
            foreach (string[] lista_elementow in wszystkieObiekty)
            {
                //Console.WriteLine(lista_elementow[max]);
                if (lista_lista.ContainsKey(lista_elementow[max]))
                {
                    lista_lista[lista_elementow[max]].Add(lista_elementow);
                }
                else
                {
                    List<string[]> temp = new List<string[]>();
                    temp.Add(lista_elementow);
                    lista_lista.Add(lista_elementow[max], temp);
                }
            }


            Console.WriteLine(entropia_atrybutu_decyzyjnego);
            foreach (KeyValuePair<string, List<string[]>> nowa_lista in lista_lista)
            {
                Console.WriteLine(nowa_lista.Key + "  " + entropia_atrybutu_decyzyjnego_funckcja(nowa_lista.Value, nowa_lista.Value.Count));
                if (entropia_atrybutu_decyzyjnego_funckcja(nowa_lista.Value, nowa_lista.Value.Count) != 0)
                {
                    Console.WriteLine(nowa_lista.Value.Count);
                    funkcja(nowa_lista.Value);
                }

            }










            Console.ReadKey();

        }//endMain
    }
}



