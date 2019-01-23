namespace MyShooter.UI
{
	public interface IData<T>
	{
		void Save(T value);
		T Load();
		void SetOptions(string path);
	}
}