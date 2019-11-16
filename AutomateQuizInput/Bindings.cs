using Ninject.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IReader>().To<Reader>();
            Bind<IQuizBuilder>().To<QuizBuilder>();
            Bind<IPageContainer>().To<PageContainer>();
            Bind<ITextChecker>().To<TextChecker>();
            Bind<IUploader>().To<Uploader>();
        }
    }
}
