using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;


namespace EasyEdit.io
{
    static class Utils
    {
        public static List<Property> GetPDF(string path)
        {
            List<Property> p = new List<Property>();
            PdfDocument document = PdfReader.Open(path);
            foreach (KeyValuePair<String, PdfItem> k in document.Info.Elements)
            {
                p.Add(new Property(k.Key, k.Value.ToString()));
            }
            return p;
        }

        public static void SetPDF(List<Property> prop, string path)
        {
            PdfDocument document = PdfReader.Open(path);
            prop = prop.Distinct().ToList();
            foreach (Property p in prop)
            {
                document.Info.Elements.Add(new KeyValuePair<String, PdfItem>('/' + p.Key, new PdfString(p.Value)));
            }
            document.Save(path);
            document.Close();
            document.Dispose();
        }
    }

    public class Property
    {
        public string Key, Value;

        public Property(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
