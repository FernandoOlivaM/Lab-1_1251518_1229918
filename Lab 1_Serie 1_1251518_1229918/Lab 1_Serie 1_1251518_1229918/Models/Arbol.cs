﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Lab_1_Serie_1_1251518_1229918.Models
{
    public class Arbol
    {
        public NodoArbol raíz;

        public Arbol()
        {
            raíz = null;
        }
        public NodoArbol ingresar(NodoArbol izquierdo, NodoArbol derecho, string nombre)
        {
            raíz = new NodoArbol();
            raíz.hijoDerecho = derecho;
            raíz.hijoIzquierdo = izquierdo;
            raíz.caracter = nombre;
            raíz.probabilidad = derecho.probabilidad + izquierdo.probabilidad;
            return raíz;
        }
        //Método para escribir el caracter  su codigo prefijo en un archivo .huff
        const int tamañoBuffer = 128;
        int espaciosUtilizados = 0;
        byte[] buffer = new byte[tamañoBuffer];

        public void generarArchivoDiccionario(string caracter, string prefijo)
        {
            string linea = $"{caracter}|{prefijo}";
            while (buffer.Length >= espaciosUtilizados)
            {
                for (int i = 0; i < linea.Length; i++)
                {
                    buffer[espaciosUtilizados] = Convert.ToByte(linea[i]);
                    espaciosUtilizados++;
               
                }
                break;
            }
            
            //using (var writeStream = new FileStream("C:\\Users\\Usuario\\Desktop\\machete.huff", FileMode.Open))
            //{
            //    using (var writer = new StreamWriter(writeStream))
            //    {
            //        writer.Write($"{caracter}|{prefijo}");
            //    }
            //}


        }
        public Dictionary<char,string> códigosPrefíjo(NodoArbol raíz, List<char> dic, Dictionary<char, string> diccionario, string códigoprefíjo)
        {
            if (raíz == null)
            {
                return diccionario;
            }
            else
            {
                diccionario = códigosPrefíjo(raíz.hijoIzquierdo, dic, diccionario, códigoprefíjo+0);
                if (raíz.hijoDerecho == null && raíz.hijoIzquierdo == null)
                {
                    foreach (char c in dic)
                    {
                        if (c == (Convert.ToChar(raíz.caracter)))
                        {
                            diccionario.Add(Convert.ToChar(raíz.caracter), códigoprefíjo);
                            //se envian los valores y llaves del diccionario a un metodo que permite la escritura en los archvios generados
                            generarArchivoDiccionario(raíz.caracter, códigoprefíjo);
                        }
                    }
                }
                diccionario = códigosPrefíjo(raíz.hijoDerecho, dic, diccionario, códigoprefíjo+1);
            }
            return diccionario;
        }
    }
}