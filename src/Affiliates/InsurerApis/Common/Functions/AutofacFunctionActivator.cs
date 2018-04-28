using Autofac;

namespace InsurerApis.Common.Functions
{
    public class AutofacFunctionActivator : IFunctionActivator
    {
        private readonly IContainer _autofacContainer;

        public AutofacFunctionActivator(Module module = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            this._autofacContainer = builder.Build();
        }

        public TFunction Activate<TFunction>(string name = null) where TFunction : IFunction
        {
            return this._autofacContainer.ResolveNamed<TFunction>(name);
        }
    }
}
