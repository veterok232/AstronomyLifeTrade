namespace Infrastructure.Constants;

internal static class DbConfigConstants
{
    public const int DefaultTextColumnLength = 255;

    public const int DefaultNameTextColumnLength = 25;

    public const int DefaultEmailColumnLength = 50;

    public const int DefaultStringifiedAddressColumnLength = 1000;

    public const int StringifiedGuidColumnLength = 36;

    public const int DefaultCommentColumnLength = 2000;

    internal static class Sequences
    {
        public const string OrderNumberSequence = "SEQ_order_number";

        public const string ReserveRequestNumberSequence = "SEQ_reserve_request_number";

        public const string PaperworkOrderNumberSequence = "SEQ_paperwork_order_number";
    }

    internal static class AgreementGoods
    {
        public const int SerialNumberColumnLength = 25;
    }
}