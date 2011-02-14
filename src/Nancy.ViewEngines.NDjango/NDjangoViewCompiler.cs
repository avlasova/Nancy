namespace Nancy.ViewEngines.NDjango
{
    using System.IO;
    using global::NDjango;
    using global::NDjango.Interfaces;
    using System;

    public class NDjangoViewCompiler : IViewCompiler, ITemplateLoader
    {
        private static volatile ITemplateManager templateManager;
        private static object syncObject = new Object();

        TemplateManagerProvider manager_provider;

        public ITemplateManager Manager
        {
            get
            {
                if (templateManager == null)
                {
                    lock (syncObject)
                    {
                        if (templateManager == null)
                            templateManager = manager_provider.GetNewManager();
                    }
                }

                return templateManager;
            }
        }


        public NDjangoViewCompiler()
        {
           manager_provider = new TemplateManagerProvider().WithLoader(this);
        }

        public NDjangoViewCompiler(Func<TemplateManagerProvider, TemplateManagerProvider> setup)
            : this()
        {
            manager_provider = setup(manager_provider).WithLoader(this);
        }

        
        
        /// <remarks>    
            /// 1) While having TextReader as an input parameter (as it was done before) we have no way to cache already parsed templates and it is difficult to track whether template was modified
            /// 2) TemplateManager should be implemented as a singleton
        ///</remarks>
        
        public IView GetCompiledView<TModel>(ViewLocationResult locationResult)
        {
 
            /*var templateManagerProvider = new TemplateManagerProvider()
                .WithLoader(new TemplateLoader(textReader));

            var templateManager = templateManagerProvider.GetNewManager();

            ITemplate template = templateManager.GetTemplate(string.Empty);
             */ 

            return new NDjangoView(locationResult.Location, Manager);
        }
        
        
        
        public System.IO.TextReader GetTemplate(string path)
        {
            return new StreamReader(path);
        }

        public bool IsUpdated(string path, System.DateTime timestamp)
        {
            return File.GetLastWriteTime(path) > timestamp;
        }

    }
}