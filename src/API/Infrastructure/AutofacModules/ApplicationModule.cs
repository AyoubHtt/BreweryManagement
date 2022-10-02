using Infrastructure.Repositories;

namespace API.Infrastructure.AutofacModules;

public class ApplicationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
    }
}
