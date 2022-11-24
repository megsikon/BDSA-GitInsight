namespace Repo;

public interface ICommitDataRepo
{

    void Create(CommitDataCreateDTO CommitData);

    IReadOnlyDictionary<string, List<Tuple<DateTime, int>>> GetAllAuthorsCommitsFromRepository(string RepositoryName);

    IReadOnlyCollection<Tuple<DateTime, int>> ReadAllCommitsFromRepo(string RepositoryName);

}