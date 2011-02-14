namespace Nancy.ViewEngines.NDjango
{
    using System;
    using System.IO;

    public static class NDjangoViewEngineExtensions
    {
        private static volatile NDjangoViewEngine viewEngine;
        private static object syncObject = new Object();


        public static NDjangoViewEngine Engine
        {
            get
            {
                if (viewEngine == null)
                {
                    lock (syncObject)
                    {
                        if (viewEngine == null)
                            viewEngine = new NDjangoViewEngine();
                    }
                }

                return viewEngine;
            }
        }

        public static Action<Stream> Django(this IViewEngine source, string name)
        {
            return source.Django<object>(name, null);
        }
       
        public static Action<Stream> Django<TModel>(this IViewEngine source, string name, TModel model)
        {
            //Why should we re-create viewEngine on each call - maybe it is better to use static member?
            //var viewEngine = new NDjangoViewEngine();

            return stream =>
            {
                //var result = viewEngine.RenderView(name, model);
                var result = Engine.RenderView(name, model);
                result.Execute(stream);
            };
        }
    }
}