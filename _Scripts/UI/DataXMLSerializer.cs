using System.IO;
using System.Xml.Serialization;

namespace MyShooter.UI
{
	public class DataXMLSerializer<T> : IData<T>
	{
		private string _path;
		private XmlSerializer _xmlSerializer;

		public void Save(T value)
		{
			if (!typeof(T).IsSerializable) return;
			using (var fs = new FileStream(_path, FileMode.Create))
			{
				_xmlSerializer.Serialize(fs, value);
			}
		}

		public T Load()
		{
			T result;
			if (!typeof(T).IsSerializable || !File.Exists(_path)) return default(T);
			using (var fs = new FileStream(_path, FileMode.Open))
			{
				result = (T)_xmlSerializer.Deserialize(fs);
			}
			return result;
		}

		public void SetOptions(string value)
		{
			_xmlSerializer = new XmlSerializer(typeof(T));
			_path = value;
		}
	}
}