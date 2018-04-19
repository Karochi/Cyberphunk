using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace CyberShooter
{
    public class XmlManager<T>
    {
        public Type type;

        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }
        public void Save(string path, object objWriter)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(type);
                xml.Serialize(writer, objWriter);
            }
        }
    }
}
