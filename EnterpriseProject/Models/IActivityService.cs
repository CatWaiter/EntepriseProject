namespace EnterpriseProject.Models
{
    public interface IActivityService
    {
        Task AddUserActivity(ActivityModel model);

        Task<List<ActivityModel>> GetUserActivity(string userId);
    }
}
