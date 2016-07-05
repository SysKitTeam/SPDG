using System;

namespace Acceleratio.SPDG.Generator
{
    public class ProgressChangedEventArgs: EventArgs
    {
        public ProgressChangeType ChangeType { get; private set; }
        public string Message { get; private set; }
        public int ProgressPctValue { get; private set; }

        public ProgressChangedEventArgs(ProgressChangeType type, string message, int progressPctValue)
        {
            ChangeType = type;
            Message = message;
            if (progressPctValue > 100)
            {
                progressPctValue = 100;
            }
            ProgressPctValue = progressPctValue;
        }
    }
}