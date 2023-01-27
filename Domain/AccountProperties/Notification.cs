using Domain.AccountProperties.UserEntity;

namespace Domain.AccountProperties;

public abstract class Notification
{
    public Notification(User sendingUser)
    {
        SendingUser = sendingUser;
        Accepted = false;
    }
    public abstract bool Accepted { get; set; }
    public abstract User SendingUser { get; set; }
    // public abstract User? ReceivingUser { get; set; }
    internal abstract void HandleNotification(User user);
}