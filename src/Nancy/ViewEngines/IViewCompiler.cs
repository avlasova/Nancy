namespace Nancy.ViewEngines
{
    using System.IO;

    public interface IViewCompiler
    {
        IView GetCompiledView<TModel>(ViewLocationResult locationResult);
    }
}