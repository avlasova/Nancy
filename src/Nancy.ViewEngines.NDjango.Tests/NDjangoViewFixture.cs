namespace Nancy.ViewEngines.NDjango.Tests
{
    using System.IO;
    using FakeItEasy;
    using global::NDjango.Interfaces;
    using Nancy.Tests;
    using Xunit;

    public class NDjangoViewFixture
    {
        [Fact]
        public void Execute()
        {
            // Given
            var templateManager = A.Fake<ITemplateManager>();
            A.CallTo(() => templateManager.RenderTemplate(null, null)).WithAnyArguments().Returns(new StringReader("view"));
            //var view = new NDjangoView(template, null) { Writer = new StringWriter() };

            var view = new NDjangoView("test.django", templateManager) {Writer = new StringWriter()};

            // When
            view.Execute();

            // Then
            view.Writer.ToString().ShouldEqual("view");
        }
    }
}