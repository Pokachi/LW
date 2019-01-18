using System;

/// <summary>
/// Notification Model containing the name of the Notification, the sender, and the payload
/// </summary>
public abstract class Notification {
    public abstract NotificationName Name { get; }
    public abstract Object Sender { get; }
    public abstract Object Payload { get; }
}
