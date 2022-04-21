using DMS.DAL.DatabaseContext;
using DMS.DAL.Interfaces;
using DMS.DAL.Repositories.MainRepo;
using Unity;
using Unity.Extension;
using Unity.Injection;
using Unity.Lifetime;

namespace DMS.DAL
{
    public class DALUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IEmployeeRepo, EmployeeRepo>();
            Container.RegisterType<IBranchesRepo, BranchesRepo>();
            Container.RegisterType<IUserRepo, UserServiceRepo>();
        }
    }
}
