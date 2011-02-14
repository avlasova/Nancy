namespace Nancy.ViewEngines.NDjango
{
    using System.Collections.Generic;
    using System.IO;
    using global::NDjango.Interfaces;

    public class NDjangoView : IView
    {
        //private readonly ITemplate template;
        private readonly string viewPath;
        private readonly ITemplateManager templateManager;

        public NDjangoView(string viewPath, ITemplateManager templateManager)
        {
            this.viewPath = viewPath;
            this.templateManager = templateManager;
        }

        public string Code { get; set; }

        public object Model { get; set; }

        public TextWriter Writer { get; set; }

        public void Execute()
        {
            var context = new Dictionary<string, object> {{"Model", Model}};
            //var reader = template.Walk(templateManager, context);
            var reader = templateManager.RenderTemplate(viewPath, context);

            Writer.Write(reader.ReadToEnd());
        }
    }
}