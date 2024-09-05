namespace Mango.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface ICommonRepositry
    {
        public ICartRepository cart {  get; }
        public void Save();
    }
}
