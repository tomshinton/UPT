using Google.Apis.Services;

namespace UPT_UI
{
    internal class DriveService
    {
        private BaseClientService.Initializer initializer;

        public DriveService(BaseClientService.Initializer initializer)
        {
            this.initializer = initializer;
        }
    }
}