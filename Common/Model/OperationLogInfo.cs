namespace Common.Model
{
    public class OperationLogInfo
    {
        public string OperatorName { get; set; } = null!;

        public DateTime OperationTime { get; set; }

        public string Info { get; set; } = null!;
    }
}
