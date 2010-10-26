using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace DipDemo.Cataloguer.Infrastructure.AppController.Environment
{
    public class FileSystem : IFileSystem
    {
        public static readonly Encoding DefaultEncoding = Encoding.Unicode;

        public void Copy(string from, string to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");

            Directory.CreateDirectory(Path.GetDirectoryName(to));

            File.Copy(from, to, true);
        }

        public void Delete(string fileName)
        {
            File.Delete(fileName);
        }

        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public IEnumerable<string> GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public IList<string> GetDirectories(string path)
        {
            if (!Directory.Exists(path))
                return new List<string>();

            return Directory.GetDirectories(path);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }

        public byte[] ReadBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string ReadText(string filePath)
        {
            return File.ReadAllText(filePath, DefaultEncoding);
        }

        public XmlDocument ReadXml(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            return doc;
        }

        public void WriteBytes(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }

        public void WriteText(string filePath, string text)
        {
            File.WriteAllText(filePath, text, DefaultEncoding);
        }

        public void WriteXml(string path, XmlDocument doc)
        {
            var declaration = doc.ChildNodes.OfType<XmlDeclaration>().SingleOrDefault();
            if (declaration == null)
            {
                declaration = doc.CreateXmlDeclaration("1.0", "utf-16", string.Empty);
                doc.InsertBefore(declaration, doc.FirstChild);
            }

            declaration.Encoding = "utf-16";

            doc.Save(path);
        }
    }
}