namespace DAL.Entity.Enums
{
    public enum OrderStatus
    {
        None,
        PendingPayment, // (>> Failed or >> OnHold)
        Failed,
        OnHold, //Awaiting payment (>> Processing)
        Processing, // Processing product (>> Completed or >> Canceled)
        Completed,
        Canceled,
        Refunded
    }
}
