// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;

namespace System.IO
{
    // Thrown when trying to access a file that doesn't exist on disk.
    public partial class FileNotFoundException : IOException
    {
        public FileNotFoundException()
            : base(SR.IO_FileNotFound)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
        }

        public FileNotFoundException(string message)
            : base(message)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
        }

        public FileNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
        }

        public FileNotFoundException(string message, string fileName) 
            : base(message)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
            FileName = fileName;
        }

        public FileNotFoundException(string message, string fileName, Exception innerException)
            : base(message, innerException)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
            FileName = fileName;
        }

        public override string Message
        {
            get
            {
                SetMessageField();
                return _message;
            }
        }

        private void SetMessageField()
        {
            if (_message == null)
            {
                if ((FileName == null) &&
                    (HResult == System.__HResults.COR_E_EXCEPTION))
                    _message = SR.IO_FileNotFound;

                else if (FileName != null)
                    _message = FileLoadException.FormatFileLoadExceptionMessage(FileName, HResult);
            }
        }

        public string FileName { get; }
        public string FusionLog { get; }

        public override string ToString()
        {
            string s = GetType().ToString() + ": " + Message;

            if (FileName != null && FileName.Length != 0)
                s += Environment.NewLine + SR.Format(SR.IO_FileName_Name, FileName);

            if (InnerException != null)
                s = s + " ---> " + InnerException.ToString();

            if (StackTrace != null)
                s += Environment.NewLine + StackTrace;

            if (FusionLog != null)
            {
                if (s == null)
                    s = " ";
                s += Environment.NewLine;
                s += Environment.NewLine;
                s += FusionLog;
            }
            return s;
        }

        protected FileNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Base class constructor will check info != null.

            FileName = info.GetString("FileNotFound_FileName");
            FusionLog = info.GetString("FileNotFound_FusionLog");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize data for our base classes.  base will verify info != null.
            base.GetObjectData(info, context);

            // Serialize data for this class
            info.AddValue("FileNotFound_FileName", FileName, typeof(string));
            info.AddValue("FileNotFound_FusionLog", FusionLog, typeof(string));
        }
    }
}

