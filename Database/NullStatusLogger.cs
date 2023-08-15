using KeePassLib.Interfaces;

namespace GateKeePass.Database
{
    public class NullStatusLogger : IStatusLogger
    {
        private string _status;

        public void StartLogging(string strOperation, bool bWriteOperationToLog)
        {
            _status = "Start: " + strOperation;
        }

        public void EndLogging()
        {
            _status = "End";
        }

        public bool SetProgress(uint uPercent)
        {
            _status = $"Progress: {uPercent}%";
            return true; // Continue operation
        }

        public bool SetText(string strNewText, LogStatusType lsType)
        {
            _status = strNewText;
            return true; // Continue operation
        }

        public bool ContinueWork()
        {
            return true; // Continue operation
        }

        public override string ToString()
        {
            return _status;
        }
    }
}
