using System;
using System.IO;
using NDjango;
using NDjango.Interfaces;

namespace Nancy.ViewEngines.NDjango
{
    public class NDjangoViewEngine : ViewEngine
    {

        public NDjangoViewEngine()
            : base(new AspNetTemplateLocator(), new NDjangoViewCompiler())
        {
        }
    }
}