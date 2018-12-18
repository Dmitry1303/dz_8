using System;
using System.Xml;
using System.Xml.Serialization;

namespace dz_8.Data
{
    [Serializable]
    public class Questions
    {
        // текст вопроса
        [XmlElement("QuestionTest")]  // тег QuestionTest
        public string Text { get; set; }

        // правда или нет
        [XmlAttribute] // атрибут - IsTrue
        public bool IsTrue { get; set; }

        // новый вопрос
        // для сериализации?
        public Questions() { }

        //  конструктор
        public Questions(string Text, bool IsTrue = true)
        {
            this.Text = Text;
            this.IsTrue = IsTrue;
        }
    }
}
