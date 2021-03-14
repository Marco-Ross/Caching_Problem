namespace Caching_
{
	public interface IRepository<ID, T>
	{
		T Save(ID key, T value);
		T Get(ID key);
	}
}