using BusinessLogicLayer.Resources.ReportResources;

namespace BusinessLogicLayer.Services.Communication
{
    public class ReportResponse : BaseResponse
    {
        public ReportResource ReportResource { get; private set; }

        private ReportResponse(bool success, string message, ReportResource reportResource) 
            : base(success, message)
        {
            ReportResource = reportResource;
        }

        public ReportResponse(ReportResource reportResource) : this(true, string.Empty, reportResource)
        {
        }

        public ReportResponse(string message) : this(false, message, null)
        {
        }
    }
}