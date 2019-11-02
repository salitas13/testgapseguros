namespace TestSegurosGAP.Utilidades
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    public static class UtilidadesGenerico
    {
        public static string LeerMensaje(string codigo)
        {
            string resourceValue = string.Empty;
            try
            {
                string filePath = AssemblyDirectory;

                XElement doc = XElement.Load(string.Format(@"{0}\Mensajes.xml", filePath));
                var result = doc.Elements("mensaje")
                                .Where(x => x.Attribute("codigo").Value == codigo)
                                .FirstOrDefault();

                resourceValue = result.FirstNode.ToString();
            }
            catch (Exception ex)
            {
                resourceValue = string.Empty;
            }

            return resourceValue;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
