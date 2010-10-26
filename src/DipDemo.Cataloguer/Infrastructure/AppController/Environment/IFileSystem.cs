using System.Collections.Generic;
using System.Xml;

namespace DipDemo.Cataloguer.Infrastructure.AppController.Environment
{
    public interface IFileSystem
    {
        void Copy(string from, string to);
        void Delete(string path);
        bool Exists(string path);
        IEnumerable<string> GetFiles(string directory);

        void CreateDirectory(string path);
        IList<string> GetDirectories(string path);
        void DeleteDirectory(string path);

        byte[] ReadBytes(string path);
        string ReadText(string path);
        XmlDocument ReadXml(string fileName);

        void WriteBytes(string path, byte[] data);
        void WriteText(string path, string text);
        void WriteXml(string path, XmlDocument doc);

    }
}