namespace CustomerPartsTracker.Shared.Models
{
    public class TrackerValidations
    {
        public const int SNMaxLength = 25;
        public const int SecondaryIDMaxLength = 25;
        public const int PONoMaxLength = 25;
        public const int ProjNoMinLength = 5;
        public const int ProjNoMaxLength = 10;

        public static (bool, string) ValidateShippedDate(Tracker t)
        {
            if (t.ShippedDate is null || t.ShippedDate >= t.ReceivedDate) return (true, "");
            else return (false, "Shipped date must not be earlier than received date.");
        }

    }
}