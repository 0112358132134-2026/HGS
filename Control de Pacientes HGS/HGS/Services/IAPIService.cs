namespace HGS.Services
{
    public interface IAPIService <T>
    {
        public Task<T?> Get(int id, string route);
        public Task<T?> SpecialGet(string route);
        public Task<IEnumerable<T>?> GetList(string route);
        public Task<HGSModel.GeneralResult?> Set(object object_to_serialize, string route);        
        public Task<HGSModel.GeneralResult?> Update(object object_to_serialize, string route);        
    }
}