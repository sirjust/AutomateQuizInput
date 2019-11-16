using System.Collections.Generic;
using OpenQA.Selenium;

namespace AutomateQuizInput
{
    public interface IUploader
    {
        IWebDriver UploadTask(List<Quiz> quizzes);
    }
}