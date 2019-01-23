using System.Xml;
using UnityEngine;

namespace MyShooter.UI
{
    public class LangManager : Singleton<LangManager>
    {
        private XmlDocument _root;
        public string LanguageCode { get; private set; }

        protected LangManager() { }

        public void Init(string file, string languageCode = "")
        {
            _root = new XmlDocument();
            if (languageCode == "")
            {
                this.LanguageCode = Application.systemLanguage ==
                SystemLanguage.Russian ? "Ru" : "En";
            }
            var config = LoadResource(file);
            if (!config) return;
            _root.LoadXml(config.text);
        }

        public string Text(string level, string id)
        {
            if (_root == null)
            {
                return "[not init]";
            }
            string path = "Settings/group[@id='" + level + "']/string[@id='" + id +
            "']/text";
            XmlNode value = _root.SelectSingleNode(path);
            if (value == null)
            {
                return "[not found]";
            }
            return value.InnerText;
        }
        private string LocalizeResourceName(string resourceName)
        {
            return resourceName + LanguageCode;
        }
        private TextAsset LoadResource(string resourceName)
        {
            return Resources.Load(LocalizeResourceName(resourceName),
            typeof(TextAsset)) as TextAsset;
        }
    }
}