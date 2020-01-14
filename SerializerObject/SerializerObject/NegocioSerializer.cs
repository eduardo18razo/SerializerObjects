using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SerializerObject
{
    public class NegocioSerializer
    {
        public class CadenaXml<T>
        {
            readonly StringBuilder _sbData;
            StringWriter _swWriter;
            XmlDocument _xDoc;
            XmlNodeReader _xNodeReader;
            XmlSerializer _xmlSerializer;
            public CadenaXml()
            {
                _sbData = new StringBuilder();
            }

            public string SerializarDatos(T datos)
            {
                try
                {
                    XmlSerializer employeeSerializer = new XmlSerializer(typeof(T));
                    _swWriter = new StringWriter(_sbData);
                    employeeSerializer.Serialize(_swWriter, datos);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return _sbData.ToString();
            }

            public T DeserializarDatos(string cadenaXml)
            {
                T resultado = default(T);
                try
                {
                    _xDoc = new XmlDocument();
                    _xDoc.LoadXml(cadenaXml);

                    if (_xDoc.DocumentElement != null)
                    {
                        _xNodeReader = new XmlNodeReader(_xDoc.DocumentElement);
                        _xmlSerializer = new XmlSerializer(typeof(T));
                        var employeeData = _xmlSerializer.Deserialize(_xNodeReader);
                        resultado = (T)employeeData;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return resultado;
            }
        }

        public class ArchivoXml<T>
        {
            public void SerializarObjeto(T objeto, string archivo)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextWriter writer = new StreamWriter(archivo))
                {
                    serializer.Serialize(writer, objeto);
                }
            }
            public void SerializarLista(List<T> lista, string archivo)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                    using (TextWriter writer = new StreamWriter(archivo))
                    {
                        serializer.Serialize(writer, lista);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public T CargarObjectoArchivo(string archivo)
            {
                T result;
                try
                {
                    XmlSerializer reader = new XmlSerializer(typeof(T));
                    StreamReader file = new StreamReader(archivo);
                    result = (T)reader.Deserialize(file);
                    file.Close();
                }
                catch (Exception ex)
                {
                    result = default(T);
                    throw new Exception(ex.Message);
                }
                return result;
            }

            public List<T> CargarListaArchivo(string archivo)
            {
                List<T> result;
                try
                {
                    XmlSerializer reader = new XmlSerializer(typeof(List<T>));
                    StreamReader file = new StreamReader(archivo);
                    result = (List<T>)reader.Deserialize(file);
                    file.Close();
                }
                catch (Exception ex)
                {
                    result = default(List<T>);
                    throw new Exception(ex.Message);
                }
                return result;
            }
        }
    }
}
