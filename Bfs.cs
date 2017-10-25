﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace algorithms
{
    public class Bfs
    {
        public Bfs(int indexPunktuStartowego)
        {
            this.IndexPunktuStartowego = indexPunktuStartowego;
            this.StosWierzcholkowDoOdwiedzenia = new Queue<int>();
            this.ListaOdwiedzonychWierzcholkow = new HashSet<int>();
        }

        private int IndexPunktuStartowego { get; }
        public Graph<int> Graf { get; set; }
        public Matrix Matrix { get; set; }
        private Queue<int> StosWierzcholkowDoOdwiedzenia { get; }
        private HashSet<int> ListaOdwiedzonychWierzcholkow { get; }

        public bool ObliczBfs()
        {
            //if (!this.ListaWierzcholkowPosiadaPunktStartowy())
            //{
            //    return true;
            //}

            this.DodajNowyWierzcholekDoOdwiedzenia(this.IndexPunktuStartowego);

            while (this.StosWierzcholkowDoOdwiedzenia.Count > 0)
            {
                var wierzcholekDoSprawdzenia = this.StosWierzcholkowDoOdwiedzenia.Dequeue();

                if (this.WierzcholeBylOdwiedzony(wierzcholekDoSprawdzenia))
                {
                    continue;
                }

                this.WirzcholekZostalOdwiedzony(wierzcholekDoSprawdzenia);

                foreach (var sasiedziWierzcholka in this.PobierzListeSasiednichWierzcholkow(wierzcholekDoSprawdzenia, DataStructureType.Matrix))
                {
                    if (!this.WierzcholeBylOdwiedzony(sasiedziWierzcholka))
                    {
                        this.DodajNowyWierzcholekDoOdwiedzenia(sasiedziWierzcholka);
                    }
                }
            }

            return true;
        }

        //public Func<int, IEnumerable<int>> ObliczNajkrotszaDroge()
        //{
        //    var poprzednieDrogi = new Dictionary<int, int>();
        //    var kolejka = new Queue<int>();

        //    kolejka.Enqueue(this.IndexPunktuStartowego);

        //    while (kolejka.Count > 0)
        //    {
        //        var aktualnyWierzcholek = kolejka.Dequeue();
        //        foreach (var sasiad in this.PobierzListeSasiednichWierzcholkow(aktualnyWierzcholek, DataStructureType.Matrix))
        //        {
        //            if (poprzednieDrogi.ContainsKey(sasiad))
        //            {
        //                continue;
        //            }

        //            poprzednieDrogi[sasiad] = aktualnyWierzcholek;
        //            kolejka.Enqueue(sasiad);
        //        }
        //    }

        //    IEnumerable<int> NajkrotszaDroga(int wierzcholek)
        //    {
        //        var sciezka = new List<int>();

        //        while (!wierzcholek.Equals(this.IndexPunktuStartowego))
        //        {
        //            sciezka.Add(wierzcholek);
        //            wierzcholek = poprzednieDrogi[wierzcholek];
        //        }

        //        sciezka.Add(this.IndexPunktuStartowego);
        //        sciezka.Reverse();

        //        return sciezka;
        //    }

        //    return NajkrotszaDroga;
        //}

        private bool ListaWierzcholkowPosiadaPunktStartowy(DataStructureType dataStructureType)
        {
            switch (dataStructureType)
            {
                case DataStructureType.Matrix:
                    return true;
                    break;

                case DataStructureType.Graf:
                    return this.Graf.ListaPowiazanychWierzcholkow.ContainsKey(this.IndexPunktuStartowego);
                    break;
            }
            return false;
        }

        private IEnumerable<int> PobierzListeSasiednichWierzcholkow(int wierzcholekDoSprawdzenia, DataStructureType dataStructureType )
        {
            var index = wierzcholekDoSprawdzenia;

            switch (dataStructureType)
            {
                case DataStructureType.Matrix:
                    this.Matrix.AddNeighbors(index);
                    var result = this.Matrix.GetAllNeighbors(index);
                    break;

                case DataStructureType.Graf:
                    //return this.Graf.ListaPowiazanychWierzcholkow[wierzcholekDoSprawdzenia];
                    break;
            }

            return null;
        }

        private void DodajNowyWierzcholekDoOdwiedzenia(int wierzcholekDoOdwiedzenia)
        {
            this.StosWierzcholkowDoOdwiedzenia.Enqueue(wierzcholekDoOdwiedzenia);
        }

        private bool WierzcholeBylOdwiedzony(int wierzcholekDoSprawdzenia)
        {
            return this.ListaOdwiedzonychWierzcholkow.Contains(wierzcholekDoSprawdzenia);
        }

        private void WirzcholekZostalOdwiedzony(int sprawdzonyWierzcholek)
        {
            this.ListaOdwiedzonychWierzcholkow.Add(sprawdzonyWierzcholek);
            Console.WriteLine("Bfs : {0}", sprawdzonyWierzcholek);
        }
    }

    public enum DataStructureType
    {
        Graf,
        Matrix
    }
}