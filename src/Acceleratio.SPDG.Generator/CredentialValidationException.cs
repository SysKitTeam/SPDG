using System;

namespace Acceleratio.SPDG.Generator
{
    public class CredentialValidationException : ApplicationException
    {
        public CredentialValidationException(string message) : base(message)
        {
        }
    }
}