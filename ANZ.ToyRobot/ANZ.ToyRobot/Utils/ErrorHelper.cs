using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ANZ.ToyRobot.Utils.Common;

namespace ANZ.ToyRobot.Utils
{
    public class ErrorMessagesHelper : IErrorMessagesHelper
    {

        public ErrorMessagesHelper()
        {
        }

        public string GetErrorMessage(ErrorType errorType)
        {
            var errors = GetErrors();

            string errorMessage = errors[errorType.ToString()];

            return errorMessage;
        }

        public Dictionary<string, string> GetErrors()
        {
            string errorMessages = string.Empty;

            var errors = GetErrorListFromFile();

            return errors;
        }

        public Dictionary<string, string> GetErrorListFromFile()
        {

            var GetDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+ "\\ErrorMessage.JSON";

            Dictionary<string, string> errors = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(GetDirectory));

            return errors;
        }
    }

    public interface IErrorMessagesHelper
    {
        public string GetErrorMessage(ErrorType errorType);
    }
}
