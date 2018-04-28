using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;


namespace MobileApps.MobileAppService.Msg
{
    public partial class index : System.Web.UI.Page
    {
     

        protected void btnSendNotifications_Click(object sender, EventArgs e)
        {
            // Get the Notification Hubs credentials for the Mobile App.
            string notificationHubName = "nhcostarica";
            string notificationHubConnection = "Endpoint=sb://notificationhcostarica.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=STGsww+qrKoNp53NsCwGGGdbPwJ2DpdCKTVd1uvmOJY=";

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // Sending the message so that all template registrations that contain "messageParam"
            // will receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            Dictionary<string, string> templateParams = new Dictionary<string, string>();

            templateParams["messageParam"] = txtToSend.Text;

            try
            {
                // Send the push notification and log the results.
                hub.SendTemplateNotificationAsync(templateParams).Wait();

                // Write the success result to the logs.
                // config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                /* Write the failure result to the logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
                    */
            }
        }
    }
}