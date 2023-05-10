namespace Core.ExecutionQueueOfNames;

public interface IExecutionQueue
{
    void AddToQueue(string login);

    bool NameAlreadyExists(string login);

    void RemoveFromQueue(string login);
}