using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_8.Data
{
    [Serializable]
    public class GameInfo
    {
        private string _Name;
        // загружаем xml
        public static GameInfo Load(string FileName)
        {
            var serializer = new XmlSerializer(typeof(GameInfo));
            using (var file = File.OpenRead(FileName))
            {
                return (GameInfo)serializer.Deserialize(file);
            }
        }
        // сохраняем в xml
        public void Save(string FileName)
        {
            var serialize = new XmlSerializer(typeof(GameInfo));
            using (var file = File.Create(FileName))
            {
                serialize.Serialize(file, this);
            }
        }

        // конструктор поля
        public string Name
        {
            get
            {
                return _Name;
            } 
            set
            {
                _Name = value;
            } 
        }

        public string Description { get; set; }

        // лист вопросов
        public List<Questions> Questions { get; set; } = new List<Questions>();
        public void Dell(int a )
        {
            Questions.Remove(Questions[a]);
        }
        //для сериализации ?
        public GameInfo() { }

        // новая игра
        public GameInfo(string Name)
        {
            _Name = Name;
        }


    }
}
