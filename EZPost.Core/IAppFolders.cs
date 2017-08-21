namespace EZPost
{
    public interface IAppFolders
    {
        string UploadHawbExcelFolder { get; }

        string EventCFileFolder { set; get; }

        string DownLoadFolder { set; get; }

        string DhlTrackingFoledr { set; get; }
    }
}