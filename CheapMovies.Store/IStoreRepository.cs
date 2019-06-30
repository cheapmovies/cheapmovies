public interface IStoreRepository
{
    void StoreValue(string key, string value);
    string GetValue(string key);
}
