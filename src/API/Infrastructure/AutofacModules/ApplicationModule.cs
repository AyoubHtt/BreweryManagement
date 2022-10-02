using Infrastructure.Repositories;

namespace API.Infrastructure.AutofacModules;

public class ApplicationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

        // Register all the Repositories classes (there name end with "Repository")
        builder.RegisterAssemblyTypes(typeof(BreweryRepository).GetTypeInfo().Assembly)
            .Where(c => c.Name.EndsWith("Repository"))
            .AsImplementedInterfaces();
    }
}
