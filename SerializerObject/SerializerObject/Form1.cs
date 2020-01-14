using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerializerObject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                List<Versionamiento> lst = new List<Versionamiento>
                {
                    new Versionamiento {Version = "1.0", Archivo = "1.jpg"},
                    new Versionamiento {Version = "2.0", Archivo = "2.jpg"},
                    new Versionamiento {Version = "3.0", Archivo = "3.jpg"},
                    new Versionamiento {Version = "4.0", Archivo = "4.jpg"},
                    new Versionamiento {Version = "5.0", Archivo = "5.jpg"},
                    new Versionamiento {Version = "6.0", Archivo = "6.jpg"},
                    new Versionamiento {Version = "7.0", Archivo = "7.jpg"}
                };
                new NegocioSerializer.ArchivoXml<Versionamiento>().SerializarLista(lst, @"C:\Prueba.xml");

                List<Versionamiento> lstArchivo =
                    new NegocioSerializer.ArchivoXml<Versionamiento>().CargarListaArchivo(@"C:\Prueba.xml");

                Versionamiento v =
                    new NegocioSerializer.ArchivoXml<Versionamiento>().CargarObjectoArchivo(@"C:\Prueba.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public class Versionamiento
        {
            public string Version { get; set; }
            public string Archivo { get; set; }
            public string Beta { get; set; }
        }
    }
}
