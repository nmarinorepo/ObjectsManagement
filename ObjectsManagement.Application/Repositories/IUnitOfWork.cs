namespace ObjectsManagement.Application.Repositories
{
    public interface IUnitOfWork
    {
        IObjectRepository ObjectRepository { get; }

        Task Save();
    }
}
