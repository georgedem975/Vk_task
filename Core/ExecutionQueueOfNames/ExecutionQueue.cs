namespace Core.ExecutionQueueOfNames;

public class ExecutionQueue : IExecutionQueue
{
    private LinkedList<string> _logins;

    public ExecutionQueue() =>
        _logins = new LinkedList<string>();

    public void AddToQueue(string login) =>
        _logins.AddLast(login);

    public bool NameAlreadyExists(string login) =>
        _logins.Contains(login);

    public void RemoveFromQueue(string login) =>
        _logins.Remove(login);
}